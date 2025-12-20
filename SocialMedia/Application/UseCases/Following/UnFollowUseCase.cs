using Application.DTOs.FollowingDTOs;
using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Following
{
    public class UnFollowUseCase
    {
        private readonly IFollowing _following;
        
        public UnFollowUseCase(IFollowing following)
        {
            _following = following;
        }
        public async Task<Result> UnFollowAsync(string unfollowedId)
        {
            var result = await _following.UnFollowAsync(unfollowedId);
            return result;
        }
    }
}
