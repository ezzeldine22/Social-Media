using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Shares
{
    public class SharePostUseCase
    {
        private readonly IPost _post;

        public SharePostUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<Result> SharePostAsync(long postId)
        {
            var result = await _post.sharePostAsync(postId);
            return result;
        }
    }
}
