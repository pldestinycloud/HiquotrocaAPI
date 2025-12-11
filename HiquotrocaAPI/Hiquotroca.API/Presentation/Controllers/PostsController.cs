using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.Application.Features.Posts.Commands.CreatePost;
using Hiquotroca.API.Application.Features.Posts.Commands.DeletePost;
using Hiquotroca.API.Application.Features.Posts.Queries.GetAllPosts;
using Hiquotroca.API.Application.Features.Posts.Queries.GetPostById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts() =>
        Ok(await _mediator.Send(new GetAllPostsQuery()));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPostById(long id)
    {
        var result = await _mediator.Send(new GetPostByIdQuery(id));
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        await _mediator.Send(new CreatePostCommand(createPostDto));
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePost(long id)
    {
        await _mediator.Send(new DeletePostCommand(id));
        return NoContent();
    }
}
