using Application.DTOs.AccountDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IUserManagerService _userManager;
        private readonly IJWTService _JWTService;

        public LoginUseCase(IUserManagerService userManager, IJWTService jWTService)
        {
            _userManager = userManager;
            _JWTService = jWTService;
        }
        public async Task<ResultT<LoginResponsesDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.findByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ResultT<LoginResponsesDto>.Failure(["Email is Wrong or not found"]);
            }

            var check_password = await _userManager.checkPasswordAsync(user, loginDto.Password);
            if (!check_password)
            {
                return ResultT<LoginResponsesDto>.Failure(["password is Wrong"]);
            }

            var claims = await _JWTService.generateClaimsAsync(user);
            await _userManager.addClaimsAsync(user, claims);

            var token = await _JWTService.generateTokenAsync(user, claims);

            var responsedData = new LoginResponsesDto
            {
                Token = token,
                Email = user.Email,
                Name = user.Name
            };
            return ResultT<LoginResponsesDto>.success(responsedData, "login successfully");
        }
    }
}
