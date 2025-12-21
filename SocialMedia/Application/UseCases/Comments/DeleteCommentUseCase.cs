using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Comments
{
    public class DeleteCommentUseCase
    {
        private readonly IPost _post;
        public DeleteCommentUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<Result> deleteCommentAsync(long commentId)
        {
            var result = await _post.deleteCommentAsync(commentId);
            return result;
        }
    }
}
