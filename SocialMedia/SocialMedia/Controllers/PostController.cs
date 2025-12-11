using Application.DTOs.PostDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _post;
        public PostController(IPost post)
        {
            _post = post;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(PostDto dto)
        {
            try
            {
                var res = await _post.CreatePostAsync(dto);
                return Ok(res);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);  
            }
        }
        [HttpGet("Get-all-posts-of-user")]
        public async Task<IActionResult> GetUserPost()
        {
            try
            {
                var res = await _post.GetPostAsync();
                return Ok(res);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-post-by-Id")]
        public async Task<IActionResult> GetPostById(long postId)
        {
            try
            {
                var res = await _post.GetPostById(postId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update-Post")]
        public async Task<IActionResult> UpdatePost(long postId, PostDto dto)
        {
            try
            {
                var res = await _post.UpdatePostAsync(postId, dto);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Post")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            try
            {
                var res = _post.DeletePostAsync(postId);
                return Ok(res.Result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}
