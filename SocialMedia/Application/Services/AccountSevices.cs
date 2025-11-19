using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.AcountDTOs;
namespace Application.Services
{
    public class AccountSevices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        public AccountSevices(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var check_exist = await _userManager.AnyAsync(p => p.Email == dto.Email);
            if (check_exist)
            {
                return "already exist user ";
            }
            string hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var new_user = new
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hash
            };
            await _userManager.AddAsync(new_user);
            await _userManager.SaveChangesAsync();
            return "User registered successfully";

        }
    }
}
