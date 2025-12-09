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
        public async Task<IActionResult> GetPosts() => 
            Ok(await _service.GetAllPostsAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPostById(long id)
        {
            var result = await _service.GetPostByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            await _service.CreatePostAsync(createPostDto);
            return NoContent();
        }

        /*[HttpPut("{id:long}")]
        public async Task<IActionResult> UpdatePost(long id, [FromBody] UpdatePostRequest updatePostRequest)
        {
        }*/

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            await _service.DeletePostAsync(id);
            return NoContent();
        }
    }
}
