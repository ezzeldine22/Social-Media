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
        private readonly UserManager<User> _userManager;

        public Following(IRepository<Follow> repo , UserManager<User> userManager)
        {
                 _repo = repo;
                 _userManager = userManager;
        }

        public async Task<Result> FollowAsync(FollowDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FollowerId) || string.IsNullOrWhiteSpace(dto.FollowedId))
            {
                return Result.Failure(new List<string> { "Invalid IDs" });
            }
            var date = DateTime.Now;
            var newFollow = new Follow
            {
                FollowerId = dto.FollowerId,
                FollowedId = dto.FollowedId,
                CreatedAt = date
            };
            var res =  await _repo.AddAsync(newFollow);
            await _repo.SaveChanges();


            if (res == null)
            {
                return Result.Failure(new List<string> { "something wrong" });
            }

            return Result.success("success following");
        }

        public async Task<Result> UnFollowAsync(FollowDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FollowedId) || string.IsNullOrWhiteSpace(dto.FollowerId))
            {
                return Result.Failure(new List<string> { "Invalid IDs" });
            }

            var res = await _repo.FirstOrDefaultAsync(f => f.FollowerId == dto.FollowerId && 
            f.FollowedId == dto.FollowedId);
            await _repo.DeleteAsync((long)res.Id);
            await _repo.SaveChanges();
            return Result.success("success unfollow");
        }
        
      
        public async Task<List<GetUserProfileDTO>> GetFollowersAsync(string userId)
        {
            var followerIds = _repo
                .Where(f => f.FollowedId == userId)
                .Select(b => b.FollowerId)
                .ToList();
            var tasks = followerIds.Select(async f => // O(1)
            {
                var user = await _userManager.FindByIdAsync(f);
                return new GetUserProfileDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    Pic = user.Pic
                };
            }).ToList();

            var followers = await Task.WhenAll(tasks);

            return followers.ToList();
        }

        public async Task<List<GetUserProfileDTO>> GetFollowingAsync(string userId)
        {
            var followerIds = _repo.Where(f => f.FollowerId == userId)
                .Select(b => b.FollowedId)
                .ToList();
            var tasks = followerIds.Select(async f =>
            {
                var user = await _userManager.FindByIdAsync(f);
                return new GetUserProfileDTO
                {

                    Name = user.Name,
                    Email = user.Email,
                    Pic = user.Pic
                };
            }).ToList();
            var followers = await Task.WhenAll(tasks);
            return followers.ToList();
        }
    }
}
