using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Post
{
    public class DeletePostUseCase
    {
        private readonly IPost _post;

        public DeletePostUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<Result> DeletePostAsync(long postId)
        {
            var result = await _post.DeletePostAsync(postId);
            return result;
        }
    }
}
