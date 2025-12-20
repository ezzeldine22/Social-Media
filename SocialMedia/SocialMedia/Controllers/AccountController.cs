using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.AccountDTOs;
using Application.UseCases.Auth;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUserCase _registerUserCase;

        public AccountController(
            LoginUseCase loginUseCase , RegisterUserCase registerUserCase)
        {
   
            _loginUseCase = loginUseCase;
            _registerUserCase = registerUserCase;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var res = await _registerUserCase.RegisterAsync(dto);
            if (res.IsSuccess)
                return Ok(res.Message);
            return BadRequest(res.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var res = await _loginUseCase.LoginAsync(loginDto);
            if (res.IsSuccess)
                return Ok(new { message = res.Message , Date = res.Data });
            return BadRequest(res.Errors);
        }

    }
}
