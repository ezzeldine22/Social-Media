using API.Domain.Entites;
using Application.DTOs.PostDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Presistence.Repositories
{
    public class PostRepo : IPost
    {
        private readonly IRepository<Post> _repo;
        private readonly IUserContext _userContext;

        public PostRepo(IRepository<Post> repo  , IUserContext userContext)
        {
            _repo = repo;
            _userContext = userContext;
        }

        public async Task<Result> CreatePostAsync(PostDto dto , string userId)
        {
            var newPost = new Post
            {
               UserId = userId,
               Caption = dto.Caption,
               ImageUrl = dto.Image_Url,
               VideoUrl = dto.Video_Url,
               CreatedAt = DateTime.Now
            };
            if(newPost == null)
            {
                return Result.Failure(new List<string> { "Failure in create post " });
            }
            
            await _repo.AddAsync(newPost);
            await _repo.SaveChanges();
            return Result.success("post added successfully");
        }
       
        public async Task<List<PostAllDetailsDtos>> GetPostAsync()
        {
            string userId = _userContext.GetUserId();
            var AllPosts = _repo.Where(u => u.UserId == userId)
                .Select(u => new PostAllDetailsDtos
                {
                    PostId = u.Id,
                    Caption = u.Caption,
                    ImageUrl = u.ImageUrl,
                    VideoUrl = u.VideoUrl,
                    date = u.CreatedAt,
                }).ToList();
            return AllPosts;
        }

        public async Task<PostAllDetailsDtos> GetPostById(long postId)
        {
            var post = await _repo.ReadById(postId);

            var postDetails = new PostAllDetailsDtos
            {
                PostId = post.Id,
                Caption = post.Caption,
                ImageUrl = post.ImageUrl,
                VideoUrl = post.VideoUrl,
                date = post.CreatedAt
            };
            return postDetails;
        }


        public async Task<Result> UpdatePostAsync(long postId , PostDto dto)
        {
            
            var post = await _repo.ReadById(postId);

            if(post == null)
            {
                return Result.Failure(new List<string> {"Post Not Found"});
            }
            post.Caption = dto.Caption;
            post.ImageUrl = dto.Image_Url;
            post.VideoUrl = dto.Video_Url;            
            await _repo.SaveChanges();
            return Result.success("update post successfully ");
        }

        
        public async Task<Result> DeletePostAsync(long postId)
        {
            var post = await _repo.ReadById(postId);
            if (post == null)
            {
                return Result.Failure(new List<string> { "Not Found post" });
            }

            await _repo.DeleteAsync((long)postId);
            await _repo.SaveChanges();
            return Result.success("post Deleted SuccessFully");
        }
    }
}
