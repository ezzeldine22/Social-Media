using Application.DTOs.PostDTOs;
using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Post
{
    public class SearchPostsUseCase
    {
        private readonly IPost _post;

        public SearchPostsUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<ResultT<IList<SearchPostsDTO>>> SearchPostsAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _post.searchPostsAsync(query, pageNumber, pageSize);
            if (!result.Any())
            {
                var errors = new List<string>();
                errors.Add("There is no posts to show");
                return ResultT<IList<SearchPostsDTO>>.Failure(errors);
            }
            return ResultT<IList<SearchPostsDTO>>.success(result);
        }
    }
}
