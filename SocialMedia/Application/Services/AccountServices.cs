using API.Domain.Entites;
using Application.DTOs.AccountDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Application.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        public AccountServices(UserManager<User> userManager  , IConfiguration configuration)
        {
            _userManager = userManager;
            _config = configuration;
        }

        public async Task<Result> RegisterAsync(RegisterDto dto)
        {
            var check_exist = await _userManager.FindByEmailAsync(dto.Email);
            if (check_exist != null)
            {
                return Result.Failure(["already exist user "]);
            }
            
            var new_user = new User
            {
                UserName = dto.Email,
                Name = dto.Name,
                Email = dto.Email
            };
            var res = await _userManager.CreateAsync(new_user, dto.Password);
            if (!res.Succeeded)
            {
                return Result.Failure(["error in register"]);
            }
            
            return Result.success("User registered successfully");
        }

        public async Task<ResultT<LoginResponsesDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user == null)
            {
                return ResultT<LoginResponsesDto>.Failure(["Email is Wrong or not found"]);
            }

            var check_password = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!check_password)
            {
                return ResultT<LoginResponsesDto>.Failure(["password is Wrong"]);
            }

            List<Claim> claims = new List<Claim>();

            
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            await _userManager.AddClaimsAsync(user , claims);
      

            SecurityKey securityKey = new SymmetricSecurityKey
                              (Encoding.UTF8.GetBytes(_config["JWT:secret"]));
            SigningCredentials signcred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signcred
            );
            var responsedData = new LoginResponsesDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Name = user.Name
            };
            return ResultT<LoginResponsesDto>.success(responsedData, "login successfully");
        }
    }
}
