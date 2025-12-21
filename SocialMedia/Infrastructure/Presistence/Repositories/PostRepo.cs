using API.Domain.Entites;
using Application.DTOs;
using Application.DTOs.PostDTOs;
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
    public class PostRepo : IPost
    {
        private readonly IRepository<Post> _repo;
        private readonly IRepository<Comment> _commentsRepo;
        private readonly IRepository<Like> _likeRepo;
        private readonly IUserContext _userContext;
        public PostRepo(IRepository<Post> repo , 
            IRepository<Comment> commentsRepo 
            , IUserContext userContext
            , IRepository<Like> likeRepo
            )
        {
            _repo = repo;
            _commentsRepo = commentsRepo;
            _userContext = userContext;
            _likeRepo = likeRepo;
        }
        public async Task<Result> CreatePostAsync(PostDto dto)
        {
            var userId = _userContext.GetUserId();
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
        public async Task<IList<SearchPostsDTO>> searchPostsAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            var post = _repo.ReadAll().Where(p =>
                EF.Functions.Like(p.Caption, $"%{query}%")
            );

            var result = await post
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(p => new SearchPostsDTO
                {
                    Caption = p.Caption,
                    Comments = p.Comments,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    VideoUrl = p.VideoUrl,
                    Likes = p.Likes,
                    Shares = p.Shares,
                    userPic = p.User.Pic,
                    userName = p.User.Name
                    
                }).ToListAsync(); 

            return result;
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
        public async Task<Result> AddCommentToPost(CommentDTO commentDTO)
        {
            var post = await _repo.ReadById(commentDTO.postID);
            if (post == null)
                return Result.Failure(new List<string>() { "not found post" });

            var userId = _userContext.GetUserId();
            var new_comment = new Comment
            {
                CreatedAt = DateTime.Now,
                PostId = commentDTO.postID,
                Text = commentDTO.comment,
                UserId = userId,
            };
            post.Comments.Add(new_comment);
            await _repo.SaveChanges();
            return Result.success("comment added successfully");
        }
        public async Task<ResultT<IEnumerable<GetCommentDTO>>> GetPostCommentsAsync(long postId)
        {
            var result = await _repo.ReadAll().
            Where(p => p.Id == postId).
            SelectMany(p => p.Comments.Select(c=> new GetCommentDTO{
                CommmentDateTime = c.CreatedAt,
                Text = c.Text,
                userId = c.UserId,
                userName = c.User.Name,
                userPic = c.User.Pic, 
                CommentID = c.Id,
            })).ToListAsync();
            return ResultT<IEnumerable<GetCommentDTO>>.success(result);
        }
        public async Task<Result> deleteCommentAsync(long commentId)
        {
            var comment = await _commentsRepo.ReadById(commentId);
            if (comment == null)
            {
                return Result.Failure(new List<string> {" No Comment found "});
            }
            await _commentsRepo.DeleteAsync(commentId);
            await _commentsRepo.SaveChanges();
            return Result.success(" Comment deleted successfully ");
        }

        // Like Services 
        public async Task<Result>LikePostAsync(long postId)
        {
            string userId = _userContext.GetUserId();
            var post = _repo.ReadById(postId);

            if(post == null)
            {
                return Result.Failure(new List<string> { "not found post " });
            }

            var newLike = new Like
            {
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            await _likeRepo.AddAsync(newLike);
            await _likeRepo.SaveChanges();
            return Result.success("success add Like  to post");
        }
        
        public async Task<Result> UnLikePostAsync(long postId)
        {
            var userId = _userContext.GetUserId();
            var rowPost = await _likeRepo.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (rowPost == null)
            {
                return Result.Failure(new List<string> { "already unlike post " });
            }
            await _likeRepo.DeleteAsync(rowPost.Id);
            await _likeRepo.SaveChanges();
            return Result.success("success unLike Post");
        }
    }
}
