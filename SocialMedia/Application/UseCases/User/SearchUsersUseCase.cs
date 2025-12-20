using Application.DTOs;
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
    public class SearchUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public SearchUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultT<IEnumerable<SearchUsersResultDTO>>> SearchUsers(string query, int pageNumber, int pageSize)
        {
            var result = await _userRepository.searchUsers(query, pageNumber, pageSize);

            if (!result.Any()) {
                var errors = new List<string>();
                errors.Add("no reuslts found");
                return ResultT<IEnumerable<SearchUsersResultDTO>>.Failure(errors);
            }
            return ResultT<IEnumerable<SearchUsersResultDTO>>.success(result);

        }
    }
}
