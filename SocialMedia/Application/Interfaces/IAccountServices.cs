using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.AccountDTOs;
using Domain.Validation;
namespace Application.Interfaces
{
    public interface IAccountServices
    {
        public Task<Result> RegisterAsync(RegisterDto dto);
        public Task<ResultT<LoginResponsesDto>> LoginAsync(LoginDto dto); 
    }
}
