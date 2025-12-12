using API.Domain.Entites;
using Application.DTOs;
using Application.DTOs.PostDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserContext _userContext;
        private readonly IRepository<Post> _repoPost;

        public UserService(UserManager<User> userManager,
            IUserContext userContext,
            IRepository<Post> repoPost)
        {
            _userManager = userManager;
            _userContext = userContext;
            _repoPost = repoPost;
        }
        public async Task<GetUserProfileDTO> GetUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                // some handling logic
                
            }
            var userProfile = new GetUserProfileDTO
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Bio = user.Bio,
                Pic = user.Pic
            };
            return userProfile;
        }

        public async Task<SearchAllDTO> searchAll(string query, int pageNumber = 1, int pageSize = 10)
        {
            var usersQuery = _userManager.Users.AsQueryable().AsNoTracking();
            var postsQuery = _repoPost.ReadAll();
            if (!string.IsNullOrEmpty(query))
            {
                usersQuery = usersQuery.Where(u =>
                    EF.Functions.Like(u.Name, $"%{query}%") ||
                    EF.Functions.Like(u.Email, $"%{query}%") ||
                    EF.Functions.Like(u.UserName, $"%{query}%")
                    );
                postsQuery = postsQuery.Where(p =>
                    EF.Functions.Like(p.Caption, $"%{query}%"));
            }

            var users = await usersQuery
                .OrderBy(u => u.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(u => new SearchUsersResultDTO
                {
                    Name = u.Name,
                    Pic = u.Pic
                })
                .ToListAsync();

            var posts = await postsQuery
                .OrderByDescending(p => p.CreatedAt)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(p => new SearchPostsDTO
                {
                    Caption = p.Caption,
                    ImageUrl = p.ImageUrl,
                    VideoUrl = p.VideoUrl,
                    CreatedAt = p.CreatedAt,
                    userName = p.User.Name,
                    userPic = p.User.Pic
                })
                .ToListAsync();

            return new SearchAllDTO
            {
                Users = users,
                Posts = posts
            };
        }

        public async Task<IList<SearchUsersResultDTO>> SearchUsers(string query, int pageNumber, int pageSize)
        {
            var res = _userManager.Users.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
             res = res.Where(u=>
                    EF.Functions.Like(u.Name , $"%{query}%")||
                    EF.Functions.Like(u.Email , $"%{query}%") ||
                    EF.Functions.Like(u.UserName , $"%{query}%")    
                    );
            }
            var users =  res.OrderBy(u => u.Name)
                .ThenBy(u => u.Email)
                .ThenBy(u => u.UserName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(u => new SearchUsersResultDTO
                {
                     Name = u.Name,
                     Pic = u.Pic
                 }).ToListAsync();

            return await users;
        }

        public async Task UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO)
        {
           string userId = _userContext.GetUserId();
           var user = await _userManager.FindByIdAsync(userId);
           if(user == null)
           {
              // handling logic
           }
           user.Name = updateUserProfileDTO.Name;
           user.Email = updateUserProfileDTO.Email;
           user.PhoneNumber = updateUserProfileDTO.Phone;
           user.Bio = updateUserProfileDTO.Bio;
           user.Pic = updateUserProfileDTO.Pic;
           await _userManager.UpdateAsync(user);
        }
    }
}
