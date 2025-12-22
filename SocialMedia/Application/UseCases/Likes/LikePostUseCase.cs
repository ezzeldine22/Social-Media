using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Likes
{
    public class LikePostUseCase
    {
        private readonly IPost _postRepo;
        public LikePostUseCase(IPost postRepo)
        {
                _postRepo = postRepo;
        }

        public async Task<Result> LikePostAsync(long postId)
        {
            var result  = await _postRepo.LikePostAsync(postId);
            return result;
        }
    }
}
