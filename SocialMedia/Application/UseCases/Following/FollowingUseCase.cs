using API.Domain.Entites;
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
    public class FollowingUseCase
    {
        private readonly IFollowing _followRepo;

        public FollowingUseCase(IFollowing following)
        {
            _followRepo = following;
        }
        public async Task<Result> FollowAsync(string followedId)
        {
            var result = await _followRepo.FollowAsync(followedId);
            return result;
        }

    }
}
