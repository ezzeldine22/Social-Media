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
    public class GetUserProfileUseCase
    {
        private readonly IUserManagerService _userManager;
        private readonly IUserContext _userContext;

        public GetUserProfileUseCase(IUserManagerService userManager , IUserContext userContext)
        {
            _userManager = userManager;
            _userContext = userContext;
        }
        public async Task<ResultT<GetUserProfileDTO>> GetUserProfile()
        {
            var id = _userContext.GetUserId();
            var user = await _userManager.findByIdAsync(id);
            if (user == null)
            {
                var errors = new List<string>();
                errors.Add("There is no users to show");
                return ResultT<GetUserProfileDTO>.Failure(errors);

            }
            var userProfile = new GetUserProfileDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Bio = user.Bio,
                Pic = user.Pic
            };
            return ResultT<GetUserProfileDTO>.success(userProfile);
        }
    }
}
