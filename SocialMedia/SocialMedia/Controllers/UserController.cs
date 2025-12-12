using Application.DTOs;
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

        [HttpGet("SearchUsers")]
        public async Task<IActionResult> SearchUsers(string query, int pageNumber = 1, int pageSize = 10)
        {
            var res = await _userService.SearchUsers(query, pageNumber, pageSize);
            return Ok(res);
        }

        [HttpGet("SearchAll")]
        public async Task<IActionResult> SearchAll(string query, int pageNumber = 1, int pageSize = 10)
        {
            var res = await _userService.searchAll(query, pageNumber, pageSize);
            return Ok(res);
        }

        [HttpPost("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDTO updateUserProfileDTO)
        {
            await _userService.UpdateUserProfile(updateUserProfileDTO);
            return Ok();
        }
    }
}
