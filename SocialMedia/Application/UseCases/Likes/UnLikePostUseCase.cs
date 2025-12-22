using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Likes
{
    public class UnLikePostUseCase
    {
        private readonly IPost _postRepo;
        public UnLikePostUseCase(IPost postRepo)
        {
                _postRepo = postRepo;
        }

        public async Task<Result> UnLikePostAsync(long postId)
        {
            var result  = await _postRepo.UnLikePostAsync(postId);
            return result;
        }
    }
}
