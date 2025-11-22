using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Entites;
using Application.DTOs.AccountDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Domain.Validation;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
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
            var claims = await _userManager.GetClaimsAsync(user);
      

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
