using API.Domain.Entites;
using Application.DTOs;
using Application.DTOs.UserDTOs;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<SearchUsersResultDTO>> searchUsers(string query, int pageNumber, int pageSize);
        Task<SearchAllDTO> searchAllAsync(string query, int pageNumber, int pageSize);
       
    }
}
