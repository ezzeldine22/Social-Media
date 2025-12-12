using Application.DTOs;
using Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<GetUserProfileDTO> GetUserProfile(string id);

        public Task<IList<SearchUsersResultDTO>> SearchUsers(string query, int pageNumber, int pageSize);

        public Task<SearchAllDTO> searchAll(string query, int pageNumber = 1, int pageSize = 10);
        public Task UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO);
    }
}
