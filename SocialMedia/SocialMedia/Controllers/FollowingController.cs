using Microsoft.AspNetCore.Mvc;
using Application.DTOs.FollowingDTOs;
using Application.UseCases.Following;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : ControllerBase
    {
        private readonly FollowingUseCase _followingUseCase;
        private readonly GetAllFollowingUseCase _getAllFollowingUseCase;
        private readonly GetAllFollowersUseCase _getAllFollowersUseCase;
        private readonly UnFollowUseCase _unFollowUseCase;

        public FollowingController(
            FollowingUseCase followingUseCase , 
            GetAllFollowingUseCase getAllFollowingUseCase ,
            GetAllFollowersUseCase getAllFollowersUseCase ,
            UnFollowUseCase unFollowUseCase)
        {

            _followingUseCase = followingUseCase;
            _getAllFollowingUseCase = getAllFollowingUseCase;
            _getAllFollowersUseCase = getAllFollowersUseCase;
            _unFollowUseCase = unFollowUseCase;
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> follow(string followedId)
        {
            var follow = await _followingUseCase.FollowAsync(followedId);
            if (follow.IsSuccess)
                return Ok(follow.Message);
            return BadRequest(follow.Errors);
        }
        [HttpDelete("unfollow")]
        public async Task<IActionResult> unfollow(string UnfollowedId)
        {
            var unfollow = await _unFollowUseCase.UnFollowAsync(UnfollowedId);
            if (unfollow.IsSuccess)
                return Ok(unfollow.Message);
            return BadRequest(unfollow.Errors);
        }
        [HttpGet("get-all-followers")]
        public async Task<IActionResult> getAllFollower()
        {
            var followers = await _getAllFollowersUseCase.GetFollowersAsync();
            if (followers.IsSuccess)
                return Ok(followers.Data);
            return BadRequest(followers.Errors);
        }
        [HttpGet("get-all-following")]
        public async Task<IActionResult> getAllFollowing()
        {
            var following = await _getAllFollowingUseCase.GetFollowingAsync();
            if (following.IsSuccess)
                return Ok(following.Data);
            return BadRequest(following.Errors);
        }
    }
}