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
    public class UpdatePostUseCase
    {
        private readonly IPost _post;

        public UpdatePostUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<Result> UpdatePostAsync(long postId, PostDto dto)
        {
            var result = await _post.UpdatePostAsync(postId, dto);
            return result;
        }
    }
}
