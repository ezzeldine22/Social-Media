using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User
{
    public class UpdateUserProfileUseCase
    {
        private readonly IUserContext _userContext;
        private readonly IUserManagerService _userManager;
       

        public UpdateUserProfileUseCase(IUserContext userContext , 
            IUserManagerService userManager
           )
        {
            _userContext = userContext;
            _userManager = userManager;
        
        }
        public async Task<Result> UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO)
        {
            string userId = _userContext.GetUserId();
            var user = await _userManager.findByIdAsync(userId);
            if (user == null)
            {
                var errors = new List<string>();
                errors.Add("There is no users to update");
                return Result.Failure(errors);
            }
            user.Name = updateUserProfileDTO.Name;
            user.Email = updateUserProfileDTO.Email;
            user.PhoneNumber = updateUserProfileDTO.Phone;
            user.Bio = updateUserProfileDTO.Bio;
            user.Pic = updateUserProfileDTO.Pic;
            await _userManager.updateAsync(user);
            return Result.success();
        }
    }
}
