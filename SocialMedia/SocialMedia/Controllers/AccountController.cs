using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.AccountDTOs;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _userAccountServices;
        public AccountController(IAccountServices userAccountServices)
        {
                _userAccountServices = userAccountServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
           var res =  await _userAccountServices.RegisterAsync(dto);
            return Ok(); 
        }

    }
}
