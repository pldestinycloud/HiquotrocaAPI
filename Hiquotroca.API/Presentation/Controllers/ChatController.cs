using Hiquotroca.API.Application.UseCases.Chats.Commands.AddUserToChat;
using Hiquotroca.API.Application.UseCases.Chats.Commands.CreateChat;
using Hiquotroca.API.Application.UseCases.Chats.Commands.DeleteChat;
using Hiquotroca.API.Application.UseCases.Chats.Commands.RemoveUserFromChat;
using Hiquotroca.API.Application.UseCases.Chats.Queries.GetMessagesByChatId;
using Hiquotroca.API.Application.UseCases.Chats.Queries.GetUserChatsWithFirstMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user-chats/{userId:long}")]
    public async Task<IActionResult> GetUserChats(long userId)
    {
        return Ok(await _mediator.Send(new GetUserChatsWithFirstMessageQuery(userId)));
    }

    [HttpGet("/messages/{chatId:long}")]
    public async Task<IActionResult> GetMessages(long chatId)
    {
        var messages = await _mediator.Send(new GetMessagesByChatIdQuery(chatId));
        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{chatId:long}")]
    public async Task<IActionResult> DeleteChat(long chatId)
    {
        await _mediator.Send(new DeleteChatCommand(chatId));
        return Ok();
    }

    /* Add and Remove users from chat endpoints are more relevant for group chats.
       If the application only supports one-on-one chats, these endpoints might not be necessary.
    
    [HttpPost("{chatId:long}/add-user/{userId:long}")]
    public async Task<IActionResult> AddUserToChat(long chatId, long userId)
    {
        await _mediator.Send(new AddUserToChatCommand(chatId, userId));
        return Ok();
    }

    [HttpDelete("{chatId:long}/remove-user/{userId:long}")]
    public async Task<IActionResult> RemoveUserFromChat(long chatId, long userId)
    {
        await _mediator.Send(new RemoveUserFromChatCommand(chatId, userId));
        return Ok();
    } */
}
