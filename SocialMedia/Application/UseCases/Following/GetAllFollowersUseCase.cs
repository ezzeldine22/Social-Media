using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Following
{
    public class GetAllFollowersUseCase
    {
        private readonly IFollowing _followRepo;
        private readonly IUserContext _userContext;

        public GetAllFollowersUseCase(IFollowing following , IUserContext userContext)
        {
            _followRepo = following;
            _userContext = userContext;
        }
        public async Task<ResultT<List<GetUserProfileDTO>>> GetFollowersAsync()
        {
            string userId = _userContext.GetUserId();
            var result = await _followRepo.GetFollowersAsync(userId);

            if (!result.Any())
            {
                var errors = new List<string>();
                errors.Add("There is no followers to show");
                return ResultT<List<GetUserProfileDTO>>.Failure(errors);
            }
            return ResultT<List<GetUserProfileDTO>>.success(result);

        }
    }
}
