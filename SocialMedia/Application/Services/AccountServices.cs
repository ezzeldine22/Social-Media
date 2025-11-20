using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Entites;
using Application.DTOs.AccountDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace Application.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        public AccountServices(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var check_exist = await _userManager.FindByEmailAsync(dto.Email);
            if (check_exist != null)
            {
                return "already exist user ";
            }
            
            var new_user = new User
            {
                UserName = dto.Email,
                Name = dto.Name,
                Email = dto.Email
            };
            var res = await _userManager.CreateAsync(new_user, dto.Password);
            if (res.Succeeded)
            {
                return "User registered successfully";
            }
            return "error in register";

        }
    }
}
