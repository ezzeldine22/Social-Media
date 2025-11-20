using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.AccountDTOs;
namespace Application.Interfaces
{
    public interface IAccountServices
    {
        public  Task<string> RegisterAsync(RegisterDto dto);
    }
}
