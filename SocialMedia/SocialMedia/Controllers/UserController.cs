using Application.DTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly SearchUsersUseCase _searchUsersUseCase;
        private readonly SearchAllUseCase _searchAllUseCase;
        private readonly UpdateUserProfileUseCase _updateProfileUseCase;
        private readonly GetUserProfileUseCase _userProfileUseCase;

        public UserController(
              SearchUsersUseCase searchUsersUserCase
            , SearchAllUseCase searchAllUseCase
            , UpdateUserProfileUseCase updateProfileUseCase
            , GetUserProfileUseCase userProfileUseCase
            )
        {
            _searchUsersUseCase = searchUsersUserCase;
            _searchAllUseCase = searchAllUseCase;
            _updateProfileUseCase = updateProfileUseCase;
            _userProfileUseCase = userProfileUseCase;
        }

        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var result = await _userProfileUseCase.GetUserProfile();
            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.Errors });
            }
            return Ok(result.Data);
        }

        [HttpGet("SearchUsers")]
        public async Task<IActionResult> SearchUsers(string query, int pageNumber = 1, int pageSize = 10)
        {
            var res = await _searchUsersUseCase.SearchUsers(query, pageNumber, pageSize);
            if (!res.IsSuccess)
            {
                return BadRequest(new { Errors = res.Errors });
            }
            return Ok(res.Data);
        }

        [HttpGet("SearchAll")]
        public async Task<IActionResult> SearchAll(string query, int pageNumber = 1, int pageSize = 10)
        {
            var res = await _searchAllUseCase.searchAllAsync(query, pageNumber, pageSize);
            if (!res.IsSuccess)
            {
                return BadRequest(new { Errors = res.Errors });
            }
            return Ok(res.Data);
        }

        [HttpPost("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDTO updateUserProfileDTO)
        {
           var result =  await _updateProfileUseCase.UpdateUserProfile(updateUserProfileDTO);

            if (!result.IsSuccess)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Message);
           
        }
    }
}
