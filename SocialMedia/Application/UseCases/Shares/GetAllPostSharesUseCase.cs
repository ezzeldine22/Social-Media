using Application.DTOs;
using Application.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Shares
{
    public class GetAllPostSharesUseCase
    {
        private readonly IPost _post;

        public GetAllPostSharesUseCase(IPost post)
        {
            _post = post;
        }
        public async Task<ResultT<IEnumerable<GetAllPostSharesDTO>>> GetAllPostSharesAsync(long postId)
        {
            var result = await _post.getAllPostSharesAsync(postId);
            if (result.Data.Any())
                return ResultT<IEnumerable<GetAllPostSharesDTO>>.success(result.Data);
            return ResultT<IEnumerable<GetAllPostSharesDTO>>.Failure(new List<string> { "No Shares Found" });
        }
    }
}
