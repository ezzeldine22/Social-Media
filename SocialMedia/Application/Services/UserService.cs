using API.Domain.Entites;
using Application.DTOs;
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
        //public async Task<GetUserProfileDTO> GetUserProfile(string id)
        //{
        //    var userProfile =  await _userManager.FindByIdAsync(id);
        //}
    }
}
