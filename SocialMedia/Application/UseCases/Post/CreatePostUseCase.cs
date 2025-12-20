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
    public class CreatePostUseCase
    {
        private readonly IPost _post;

        public CreatePostUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<Result> CreatePostAsync(PostDto postDto)
        {
            var result = await _post.CreatePostAsync(postDto);
            return result;
        }
    }
}
