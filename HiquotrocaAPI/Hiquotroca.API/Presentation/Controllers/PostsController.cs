using Hiquotroca.API.Application.Features.Posts.Commands.AddUserToFavoritePost;
using Hiquotroca.API.Application.Features.Posts.Commands.RemoveUserFromFavoritePost;
using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.Application.Features.Posts.Commands.CreatePost;
using Hiquotroca.API.Application.Features.Posts.Commands.DeletePost;
using Hiquotroca.API.Application.Features.Posts.Queries.GetAllPosts;
using Hiquotroca.API.Application.Features.Posts.Queries.GetPostById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hiquotroca.API.Application.Features.Posts.Queries.GetUserFavoritePosts;

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
    public async Task<IActionResult> GetPosts()
    {
        var result = await _mediator.Send(new GetAllPostsQuery());
        if (result == null || !result.Any())
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPostById(long id)
    {
        var result = await _mediator.Send(new GetPostByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePost(long id)
    {
        await _mediator.Send(new DeletePostCommand(id));
        return NoContent();
    }

    [HttpGet("/favorites/{userId:long}")]
    public async Task<IActionResult> GetFavoritePostsByUserId(long userId)
    {
        var result = await _mediator.Send(new GetUserFavoritePostsQuery(userId));
        if (result == null || !result.Any())
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{postId:long}/add-favorite/{userId:long}")]
    public async Task<IActionResult> AddUserToFavorite(long postId, long userId)
    {
        await _mediator.Send(new AddUserToFavoritePostCommand(postId, userId));
        return NoContent();
    }

    [HttpDelete("{postId:long}/remove-favorite/{userId:long}")]
    public async Task<IActionResult> RemoveUserFromFavorite(long postId, long userId)
    {
       await _mediator.Send(new RemoveUserFromFavoritePostCommand(postId, userId));
       return Ok();
    }
}
