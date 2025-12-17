using Hiquotroca.API.Application.UseCases.Users.Commands.CreateUser;
using Hiquotroca.API.Application.UseCases.Users.Commands.DeleteUser;
using Hiquotroca.API.Application.UseCases.Users.Commands.UpdateUser;
using Hiquotroca.API.Application.UseCases.Users.Commands.FollowUser;
using Hiquotroca.API.Application.UseCases.Users.Commands.UnfollowUser;
using Hiquotroca.API.Application.UseCases.Users.Queries.GetAllUsers;
using Hiquotroca.API.Application.UseCases.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hiquotroca.API.Application.UseCases.Users.Queries.GetFollowingUsers;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers() =>
        Ok(await _mediator.Send(new GetAllUsersQuery()));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        await _mediator.Send(updateUserCommand);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }

    [HttpGet("{userId:long}/followers")]
    public async Task<IActionResult> GetFollowers(long userId)
    {
        var followers = await _mediator.Send(new GetFollowingUsersQuery(userId));

        if (followers == null || !followers.Any())
            return NotFound();

        return Ok(followers);
    }

    [HttpPost("{userId:long}/followers/{targetUserId:long}")]
    public async Task<IActionResult> FollowUser(long userId, long targetUserId)
    {
        await _mediator.Send(new FollowUserCommand(userId, targetUserId));
        return NoContent();
    }

    [HttpDelete("{userId:long}/followers/{targetUserId:long}")]
    public async Task<IActionResult> UnfollowUser(long userId, long targetUserId)
    {
        await _mediator.Send(new UnfollowUserCommand(userId, targetUserId));
        return NoContent();
    }
}
