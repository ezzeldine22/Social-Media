using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs.FollowingDTOs;
using Application.Interfaces;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : ControllerBase
    {
        private readonly IFollowing _followingServices;
        public FollowingController(IFollowing followingServices)
        {

            _followingServices = followingServices;
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> follow(FollowDto dto)
        {
            var follow = await _followingServices.FollowAsync(dto);
            if (follow.IsSuccess)
            {
                return Ok(follow);
            }
            else
            {
                return BadRequest(follow.Errors);
            }

        }
        [HttpDelete("unfollow")]
        public async Task<IActionResult> unfollow(FollowDto dto)
        {
            var unfollow = await _followingServices.UnFollowAsync(dto);
            if (unfollow.IsSuccess)
            {
                return Ok(unfollow);
            }
            else
            {
                return BadRequest(unfollow.Errors);
            }
        }
        [HttpGet("get-all-followers")]
        
        public async Task<IActionResult> getAllFollower(string userId)
        {
            try
            {
                var followers = await _followingServices.GetFollowersAsync(userId);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("get-all-following")]

        public async Task<IActionResult> getAllFollowing(string userId)
        {
            try
            {
                var followers = await _followingServices.GetFollowingAsync(userId);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}