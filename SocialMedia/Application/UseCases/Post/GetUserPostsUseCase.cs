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
    public class GetUserPostsUseCase
    {
        private readonly IPost _post;

        public GetUserPostsUseCase(IPost post)
        {
            _post = post;
        }

        public async Task<ResultT<List<PostAllDetailsDtos>>> GetUserPostsAsync()
        {
            var result = await _post.GetPostAsync();

            if (!result.Any())
            {
                var errors = new List<string>();
                errors.Add("There is no posts to show");
                return ResultT<List<PostAllDetailsDtos>>.Failure(errors);
            }
            return ResultT<List<PostAllDetailsDtos>>.success(result);
        }
    }
}
