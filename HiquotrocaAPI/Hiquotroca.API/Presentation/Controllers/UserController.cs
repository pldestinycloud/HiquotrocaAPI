using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() =>
            Ok(await _service.GetAllUsersAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _service.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            await _service.UpdateUserAsync(id, updateUserDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _service.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("{userId:long}/favorites/{postId:long}")]
        public async Task<IActionResult> AddFavoritePost(long userId, long postId)
        {
            await _service.AddFavoritePostAsync(userId, postId);
            return NoContent();
        }

        [HttpDelete("{userId:long}/favorites/{postId:long}")]
        public async Task<IActionResult> RemoveFavoritePost(long userId, long postId)
        {
            await _service.RemoveFavoritePostAsync(userId, postId);
            return NoContent();
        }

        [HttpPost("{userId:long}/follow/{targetUserId:long}")]
        public async Task<IActionResult> FollowUser(long userId, long targetUserId)
        {
            await _service.FollowUserAsync(userId, targetUserId);
            return NoContent();
        }

        [HttpDelete("{userId:long}/follow/{targetUserId:long}")]
        public async Task<IActionResult> UnfollowUser(long userId, long targetUserId)
        {
            await _service.UnfollowUserAsync(userId, targetUserId);
            return NoContent();
        }

        [HttpPost("{userId:long}/promotional-codes/{promotionalCodeId:long}")]
        public async Task<IActionResult> ApplyPromoCode(long userId, long promotionalCodeId)
        {
            await _service.AttributePromotionalCode(userId, promotionalCodeId);
            return NoContent();
        }

        [HttpDelete("{userId:long}/promotional-codes/{promotionalCodeId:long}")]
        public async Task<IActionResult> RemovePromoCode(long userId, long promotionalCodeId)
        {
            await _service.RemovePromotionalCode(userId, promotionalCodeId);
            return NoContent();
        }
    }
}
