using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Entites;
using Application.DTOs.FollowingDTOs;
using Application.DTOs.UserDTOs;
using Domain.Validation;
namespace Application.Interfaces
{
    public interface IFollowing
    {
        public Task<Result> FollowAsync(string followedId);
        public Task<Result> UnFollowAsync(string UnfollowedId);  
        public Task<List<GetUserProfileDTO>> GetFollowersAsync(string userId);
        public Task<List<GetUserProfileDTO>> GetFollowingAsync(string userId);
    }
}
