using Application.DTOs;
using Application.DTOs.PostDTOs;
using Application.Interfaces;
using Application.UseCases.Comments;
using Application.UseCases.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly CreatePostUseCase _createPostUseCase;
        private readonly DeletePostUseCase _deletePostUseCase;
        private readonly GetPostByIDUseCase _getPostByIDUseCase;
        private readonly GetUserPostsUseCase _getUserPostsUseCase;
        private readonly SearchPostsUseCase _searchPostsUseCase;
        private readonly UpdatePostUseCase _updatePostUseCase;
        private readonly AddCommentUseCase _addCommentUseCase;
        private readonly GetPostCommentsUseCase _getPostCommentsUseCase;
        private readonly DeleteCommentUseCase _deleteCommentUseCase;
        public PostController(
            CreatePostUseCase createPostUseCase , 
            DeletePostUseCase deletePostUseCase , 
            GetPostByIDUseCase getPostByIDUseCase , 
            GetUserPostsUseCase getUserPostsUseCase ,
            SearchPostsUseCase searchPostsUseCase ,
            UpdatePostUseCase updatePostUseCase,
            AddCommentUseCase addCommentUseCase,
            GetPostCommentsUseCase getPostCommentsUseCase,
            DeleteCommentUseCase deleteCommentUseCase)
        {
            _createPostUseCase = createPostUseCase;
            _deletePostUseCase = deletePostUseCase;
            _getPostByIDUseCase = getPostByIDUseCase;
            _getUserPostsUseCase = getUserPostsUseCase;
            _searchPostsUseCase = searchPostsUseCase;
            _updatePostUseCase = updatePostUseCase;
            _addCommentUseCase = addCommentUseCase;
            _getPostCommentsUseCase = getPostCommentsUseCase;
            _deleteCommentUseCase = deleteCommentUseCase;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(PostDto dto)
        {
            var res = await _createPostUseCase.CreatePostAsync(dto);
            if (res.IsSuccess)
                return Ok(res.Message);
            return BadRequest(res.Errors);
        }
        [HttpGet("Get-all-posts-of-user")]
        public async Task<IActionResult> GetUserPost()
        {
            var res = await _getUserPostsUseCase.GetUserPostsAsync();
            if (res.IsSuccess)
                return Ok(res.Data);
            return BadRequest(res.Errors);
        }

        [HttpGet("Get-post-by-Id")]
        public async Task<IActionResult> GetPostById(long postId)
        {
            var res = await _getPostByIDUseCase.GetPostByIdAsync(postId);
            if (res.IsSuccess)
                return Ok(res.Data);
            return BadRequest(res.Errors);
        }
        [HttpPut("Update-Post")]
        public async Task<IActionResult> UpdatePost(long postId, PostDto dto)
        {
            var res = await _updatePostUseCase.UpdatePostAsync(postId, dto);
            if (res.IsSuccess)
                return Ok(res.Message);
            return BadRequest(res.Errors);
        }

        [HttpDelete("Delete-Post")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            var res = await _deletePostUseCase.DeletePostAsync(postId);
            if (res.IsSuccess)
                return Ok(res.Message);
            return BadRequest(res.Errors);
        }

        [HttpPost("SearchPosts")]
        public async Task<IActionResult> searchPosts(string query, int pageNumber = 1, int pageSize=10)
        {
            var result = await _searchPostsUseCase.SearchPostsAsync(query, pageNumber, pageSize); 
            if (result.IsSuccess)
            return Ok(result.Data);
            return BadRequest(result.Errors);
        }

        [HttpPost("AddCommentToPost")]
        public async Task<IActionResult> AddCommentToPost(CommentDTO commentDTO)
        {
            var result = await _addCommentUseCase.addCommentAsync(commentDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Message);
        }

        [HttpGet("GetComments")]
        public async Task<IActionResult> GetPostComments(long postId)
        {
            var result =  await _getPostCommentsUseCase.GetPostCommentsAsync(postId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new {comments = result.Data});
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> deleteComment(int commentID)
        {
            var result = await _deleteCommentUseCase.deleteCommentAsync(commentID);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }
    }
}
