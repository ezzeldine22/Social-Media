using API.Domain.Entites;
using Application.DTOs.FollowingDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Presistence.Repositories
{
    public class Following : IFollowing
    {

        private readonly IRepository<Follow> _repo;
        private readonly IUserContext _userContext;
        private readonly UserManager<User> _userManager;

        public Following(IRepository<Follow> repo,IUserContext userContext ,UserManager<User> userManager)
        {
            _repo = repo;
            _userContext = userContext;
            _userManager = userManager;
        }

        public async Task<Result> FollowAsync(string followedId)
        {
            string followingId = _userContext.GetUserId();
            if (string.IsNullOrWhiteSpace(followedId))
            {
                return Result.Failure(new List<string> { "Invalid ID" });
            }
            var date = DateTime.Now;
            var newFollow = new Follow
            {
                FollowerId = followingId,
                FollowedId = followedId,
                CreatedAt = date
            };
            var res = await _repo.AddAsync(newFollow);
            await _repo.SaveChanges();
            if (res == null)
            {
                return Result.Failure(new List<string> { "something wrong" });
            }
            return Result.success("success following");
        }

        public async Task<Result> UnFollowAsync(string UnfollowedId)
        {
            if (string.IsNullOrWhiteSpace(UnfollowedId))
            {
                return Result.Failure(new List<string> { "Invalid ID" });
            }
            var unfollowingId = _userContext.GetUserId();
            var res = await _repo.FirstOrDefaultAsync(f => f.FollowerId == unfollowingId &&
            f.FollowedId == UnfollowedId);
            await _repo.DeleteAsync((long)res.Id);
            await _repo.SaveChanges();
            return Result.success("success unfollow");
        }
        public async Task<List<GetUserProfileDTO>> GetFollowersAsync(string userId)
        {
            var followerIds = _repo
          .Where(f => f.FollowedId == userId)
          .Select(f => f.FollowerId)
          .ToList();
            var followers = await _userManager.Users
                .Where(u => followerIds.Contains(u.Id))
                .Select(u => new GetUserProfileDTO
                {
                    Name = u.Name,
                    Email = u.Email,
                    Pic = u.Pic
                })
                .ToListAsync();

            return followers;
        }
        public async Task<List<GetUserProfileDTO>> GetFollowingAsync(string userId)
        {
            var followerIds = _repo.Where(f => f.FollowerId == userId)
                .Select(b => b.FollowedId)
                .ToList();
            var followers = await _userManager.Users
                .Where(f => followerIds.Contains(f.Id))
                .Select(b => new GetUserProfileDTO
                {
                    Name = b.Name,
                    Email = b.Email,
                    Pic = b.Pic
                }).ToListAsync();

            return followers;   
        }
    }
}
