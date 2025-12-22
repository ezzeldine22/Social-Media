using Application.DTOs;
using Application.DTOs.PostDTOs;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPost
    {
        Task<Result> sharePostAsync(long postId);
        Task<Result> UnsharePostAsync(long postId);
        Task<ResultT<IEnumerable<GetAllPostSharesDTO>>> getAllPostSharesAsync(long postId);
        Task<ResultT<IEnumerable<GetCommentDTO>>> GetPostCommentsAsync(long postId);
        Task<Result> deleteCommentAsync(long commentId);
        public Task<Result> CreatePostAsync(PostDto dto );
        public Task<List<PostAllDetailsDtos>> GetPostAsync();
        Task<Result> AddCommentToPost(CommentDTO commentDTO);
        public Task<PostAllDetailsDtos> GetPostById(long postId);
        public Task<Result> UpdatePostAsync(long postId , PostDto dto);
        public Task<Result> DeletePostAsync(long postId);
        public Task<Result> LikePostAsync(long postId);
        public Task<Result> UnLikePostAsync(long postId);
        public Task<IList<SearchPostsDTO>>searchPostsAsync(string query, int pageNumber = 1, int pageSize = 10);
    }
}
