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
    public class GetAllFollowingUseCase
    {
        private readonly IFollowing _followRepo;
        private readonly IUserContext _userContext;

        public GetAllFollowingUseCase(IFollowing following, IUserContext userContext)
        {
            _followRepo = following;
            _userContext = userContext;
        }
        public async Task<ResultT<List<GetUserProfileDTO>>> GetFollowingAsync()
        {
            string userId = _userContext.GetUserId();
            var result = await _followRepo.GetFollowingAsync(userId);

            if (!result.Any())
            {
                var errors = new List<string>();
                errors.Add("There is no followed to show");
                return ResultT<List<GetUserProfileDTO>>.Failure(errors);
            }
            return ResultT<List<GetUserProfileDTO>>.success(result);

        }
    }
}
