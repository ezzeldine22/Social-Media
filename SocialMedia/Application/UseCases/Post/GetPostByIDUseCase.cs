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
    public class GetPostByIDUseCase
    {
        private readonly IPost _post;

        public GetPostByIDUseCase(IPost post)
        {
            _post = post;
        }

        public async Task<ResultT<PostAllDetailsDtos>> GetPostByIdAsync(long postId)
        {
            var result = await _post.GetPostById(postId);

            if (result == null)
            {
                var errors = new List<string>();
                errors.Add("There is no post to show");
                return ResultT<PostAllDetailsDtos>.Failure(errors);
            }
            return ResultT<PostAllDetailsDtos>.success(result);
        }
    }
}
