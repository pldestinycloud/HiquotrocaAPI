using Hiquotroca.API.Application.Features.Users.Commands.AddFavoritePost;
using Hiquotroca.API.Application.Features.Users.Commands.DeleteUser;
using Hiquotroca.API.Application.Features.Users.Commands.RemoveFavoritePost;
using Hiquotroca.API.Application.Features.Users.Commands.UpdateUser;
using Hiquotroca.API.Application.Features.Users.Commands.FollowUser;
using Hiquotroca.API.Application.Features.Users.Commands.UnfollowUser;
using Hiquotroca.API.Application.Features.Users.Commands.AttributePromotionalCode;
using Hiquotroca.API.Application.Features.Users.Commands.RemovePromotionalCode;
using Hiquotroca.API.Application.Features.Users.Queries.GetAllUsers;
using Hiquotroca.API.Application.Features.Users.Queries.GetUserById;
using Hiquotroca.API.Application.Features.Users.Queries.GetUserFavoritePosts;
using Hiquotroca.API.Application.Features.Users.Queries.GetUserFollowers;
using Hiquotroca.API.Application.Features.Users.Queries.GetUserPromotionalCodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateUser(long id, [FromBody] DTOs.Users.Requests.UpdateUserDto updateUserDto)
    {
        await _mediator.Send(new UpdateUserCommand(id, updateUserDto));
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }

    [HttpGet("{userId:long}/favorite-posts")]
    public async Task<IActionResult> GetFavoritePosts(long userId)
    {
        var favoritePosts = await _mediator.Send(new GetUserFavoritePostsQuery(userId));
        if (favoritePosts == null || !favoritePosts.Any())
            return NotFound();
        return Ok(favoritePosts);
    }

    [HttpPost("{userId:long}/favorite-posts/{postId:long}")]
    public async Task<IActionResult> AddFavoritePost(long userId, long postId)
    {
        await _mediator.Send(new AddFavoritePostCommand(userId, postId));
        return NoContent();
    }

    [HttpDelete("{userId:long}/favorite-posts/{postId:long}")]
    public async Task<IActionResult> RemoveFavoritePost(long userId, long postId)
    {
        await _mediator.Send(new RemoveFavoritePostCommand(userId, postId));
        return NoContent();
    }

    [HttpGet("{userId:long}/followers")]
    public async Task<IActionResult> GetFollowers(long userId)
    {
        var followers = await _mediator.Send(new GetUserFollowersQuery(userId));
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

    [HttpGet("{userId:long}/promotional-codes")]
    public async Task<IActionResult> GetPromotionalCodes(long userId)
    {
        var userPromotionalCodesIds = await _mediator.Send(new GetUserPromotionalCodesQuery(userId));
        if(userPromotionalCodesIds == null || !userPromotionalCodesIds.Any())
            return NotFound();
        // Aqui podes chamar outro handler/serviço para buscar os detalhes dos códigos
        return Ok(userPromotionalCodesIds);
    }

    [HttpPost("{userId:long}/promotional-codes/{promotionalCodeId:long}")]
    public async Task<IActionResult> ApplyPromoCode(long userId, long promotionalCodeId)
    {
        await _mediator.Send(new AttributePromotionalCodeCommand(userId, promotionalCodeId));
        return NoContent();
    }

    [HttpDelete("{userId:long}/promotional-codes/{promotionalCodeId:long}")]
    public async Task<IActionResult> RemovePromoCode(long userId, long promotionalCodeId)
    {
        await _mediator.Send(new RemovePromotionalCodeCommand(userId, promotionalCodeId));
        return NoContent();
    }
}
