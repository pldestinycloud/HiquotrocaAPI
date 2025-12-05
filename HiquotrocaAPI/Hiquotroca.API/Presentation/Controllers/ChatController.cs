using Hiquotroca.API.Application.Services;
using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.DTOs.Chat.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hiquotroca.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly ChatService _service;
        public ChatsController(ChatService service)
        {
            _service = service;
        }

        [HttpGet("user-chats/{userId:long}")]
        public async Task<IActionResult> GetUserChats(long userId)
        {
            var result = await _service.GetUserChatsWithFirstMessageAsync(userId);
            if (!result.isSuccess)
                return BadRequest(result);
            if (result.Data == null || !result.Data.Any())
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("{chatId:long}")]
        public async Task<IActionResult> GetMessages(long chatId)
        {
            var result = await _service.GetMessagesByChatIdAsync(chatId);
            if (!result.isSuccess)
                return BadRequest(result);
            if (result.Data == null || !result.Data.Any())
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto)
        {
            var result = await _service.CreateChatAsync(dto);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("{chatId:long}/add-user/{userId:long}")]
        public async Task<IActionResult> AddUserToChat(long chatId, long userId)
        {
            var result = await _service.AddUserToChatAsync(chatId, userId);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{chatId:long}/remove-user/{userId:long}")]
        public async Task<IActionResult> RemoveUserFromChat(long chatId, long userId)
        {
            var result = await _service.RemoveUserFromChatAsync(chatId, userId);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{chatId:long}")]
        public async Task<IActionResult> DeleteChat(long chatId)
        {
            var result = await _service.DeleteChatAsync(chatId);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
