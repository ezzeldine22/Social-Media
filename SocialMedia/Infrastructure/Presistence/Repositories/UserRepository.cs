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

namespace Infrastructure.Presistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediaContext _dbContext;
       

        public UserRepository(SocialMediaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SearchAllDTO> searchAllAsync(string query, int pageNumber, int pageSize)
        {
            var usersQuery = _dbContext.Users.AsQueryable().AsNoTracking();
            var postsQuery = _dbContext.Posts.AsQueryable().AsNoTracking();

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

        public async Task<IEnumerable<SearchUsersResultDTO>> searchUsers(string query, int pageNumber, int pageSize)
        {
            var res = _dbContext.Users.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                res = res.Where(u =>
                       EF.Functions.Like(u.Name, $"%{query}%") ||
                       EF.Functions.Like(u.Email, $"%{query}%") ||
                       EF.Functions.Like(u.UserName, $"%{query}%")
                       );
            }
            var users = await res.OrderBy(u => u.Name)
                .ThenBy(u => u.Email)
                .ThenBy(u => u.UserName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(u => new SearchUsersResultDTO
                {
                    Name = u.Name,
                    Pic = u.Pic
                }).ToListAsync();
            return users;
        }

    }
}
