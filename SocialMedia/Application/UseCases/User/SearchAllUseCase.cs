using Application.DTOs;
using Application.DTOs.PostDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User
{
    public class SearchAllUseCase
    {
        private readonly IUserRepository _userRepository;

        public SearchAllUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultT<SearchAllDTO>> searchAllAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
          var result = await _userRepository.searchAllAsync(query, pageNumber, pageSize);

            if(result.Posts.Any() || result.Users.Any())
                return ResultT<SearchAllDTO>.success(result);
            var errors = new List<string>();
            errors.Add("there is no results to show");
            return ResultT<SearchAllDTO>.Failure(errors);
        }
    }
}
