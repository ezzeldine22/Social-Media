using Application.DTOs.PostDTOs;
using Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SearchAllDTO
    {
       
        public IList<SearchPostsDTO> Posts { get; set; }
        public IList<SearchUsersResultDTO> Users { get; set; }
    }
}
