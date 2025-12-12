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
        public Task<Result> CreatePostAsync(PostDto dto );
        public Task<List<PostAllDetailsDtos>> GetPostAsync();
        public Task<PostAllDetailsDtos> GetPostById(long postId);
        public Task<Result> UpdatePostAsync(long postId , PostDto dto);
        public Task<Result> DeletePostAsync(long postId);

        public Task<IList<SearchPostsDTO>>searchPostsAsync(string query, int pageNumber = 1, int pageSize = 10);
    }
}
