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
    public class GetPostCommentsUseCase
    {
        private readonly IPost _postRepo;
        public GetPostCommentsUseCase(IPost postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task<ResultT<IEnumerable<GetCommentDTO>>> GetPostCommentsAsync(long postId)
        {
            var result = await _postRepo.GetPostCommentsAsync(postId);
            return result;
        }
    }
}
