using Application.DTOs;
using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Comments
{
    public class AddCommentUseCase
    {
        private readonly IPost _postRepo;

        public AddCommentUseCase(IPost postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task<Result> addCommentAsync(CommentDTO commentDTO)
        {
            var result = await _postRepo.AddCommentToPost(commentDTO);
            return result;
        }
    }
}
