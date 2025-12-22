using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Shares
{
    public class UnSharePostUseCase
    {
        private readonly IPost _post;

        public UnSharePostUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<Result> UnSharePostAsync(long postId)
        {
            var result = await _post.UnsharePostAsync(postId);
            return result;
        }
    }
}
