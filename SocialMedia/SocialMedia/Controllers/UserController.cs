using Application.DTOs.UserDTOs;
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

        
    }
}
