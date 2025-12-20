using API.Domain.Entites;
using Application.DTOs.AccountDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Auth
{
    public class RegisterUserCase
    {
        private readonly IUserManagerService _userManager;

        public RegisterUserCase(IUserManagerService userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result> RegisterAsync(RegisterDto dto)
        {
            var check_exist = await _userManager.findByEmailAsync(dto.Email);
            if (check_exist != null)
            {
                return Result.Failure(["already exist user "]);
            }
            var new_user = new API.Domain.Entites.User
            {
                UserName = dto.Email,
                Name = dto.Name,
                Email = dto.Email
            };
            var res = await _userManager.createAsync(new_user, dto.Password);
            if (!res.Succeeded)
            {
                return Result.Failure(["error in register"]);
            }

            return Result.success("User registered successfully");
        }
    }
}
