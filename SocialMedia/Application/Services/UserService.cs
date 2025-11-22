using API.Domain.Entites;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<GetUserProfileDTO> GetUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                // some handling logic
                
            }
            var userProfile = new GetUserProfileDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Bio = user.Bio,
                Pic = user.Pic
            };
            return userProfile;
        }
    }
}
