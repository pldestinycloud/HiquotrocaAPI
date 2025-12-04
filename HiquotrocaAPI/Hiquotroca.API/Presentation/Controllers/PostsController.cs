using Hiquotroca.API.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.DTOs.Posts;

namespace Hiquotroca.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService _service;
        public PostsController(PostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var result = await _service.GetAllPostsAsync();

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.Data == null || !result.Data.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPostById(long id)
        {
            var result = await _service.GetPostByIdAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.Data == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest createPostRequest)
        {
            var result = await _service.CreatePostAsync(createPostRequest);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdatePost(long id, [FromBody] UpdatePostRequest updatePostRequest)
        {
            var result = await _service.UpdatePostAsync(id, new PostDto());

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.Data == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            var result = await _service.DeletePostAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
