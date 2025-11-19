using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.AcountDTOs;
namespace Application.Interfaces
{
    public interface IAccountServices
    {
        public Task<string> RegisterAsync(RegisterDto dto);
    }
}
