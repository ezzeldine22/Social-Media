using Application.DTOs.UserDTOs;
using Application.DTOs.AccountDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUserProfile/{id}")]
        public async Task<GetUserProfileDTO> GetUserProfile(string id)
        {
            return await _userService.GetUserProfile(id);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto);
            return Ok(new { message = result });
        } 
        
    }
}
