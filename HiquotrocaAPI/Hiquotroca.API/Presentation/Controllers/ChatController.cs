using Hiquotroca.API.Application.Features.Chats.Commands.AddUserToChat;
using Hiquotroca.API.Application.Features.Chats.Commands.CreateChat;
using Hiquotroca.API.Application.Features.Chats.Commands.DeleteChat;
using Hiquotroca.API.Application.Features.Chats.Commands.RemoveUserFromChat;
using Hiquotroca.API.Application.Features.Chats.Queries.GetMessagesByChatId;
using Hiquotroca.API.Application.Features.Chats.Queries.GetUserChatsWithFirstMessage;
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
        var chats = await _mediator.Send(new GetUserChatsWithFirstMessageQuery(userId));
        if (chats == null || !chats.Any())
            return NotFound();
        return Ok(chats);
    }

    [HttpGet("{chatId:long}")]
    public async Task<IActionResult> GetMessages(long chatId)
    {
        var messages = await _mediator.Send(new GetMessagesByChatIdQuery(chatId));
        if (messages == null || !messages.Any())
            return NotFound();
        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] DTOs.Chat.Requests.CreateChatDto dto)
    {
        var chatId = await _mediator.Send(new CreateChatCommand(dto));
        if (chatId == null)
            return BadRequest();
        return Ok(chatId);
    }

    [HttpPost("{chatId:long}/add-user/{userId:long}")]
    public async Task<IActionResult> AddUserToChat(long chatId, long userId)
    {
        var success = await _mediator.Send(new AddUserToChatCommand(chatId, userId));
        if (!success)
            return BadRequest();
        return Ok();
    }

    [HttpDelete("{chatId:long}/remove-user/{userId:long}")]
    public async Task<IActionResult> RemoveUserFromChat(long chatId, long userId)
    {
        var success = await _mediator.Send(new RemoveUserFromChatCommand(chatId, userId));
        if (!success)
            return BadRequest();
        return Ok();
    }

    [HttpDelete("{chatId:long}")]
    public async Task<IActionResult> DeleteChat(long chatId)
    {
        var success = await _mediator.Send(new DeleteChatCommand(chatId));
        if (!success)
            return BadRequest();
        return Ok();
    }
}
