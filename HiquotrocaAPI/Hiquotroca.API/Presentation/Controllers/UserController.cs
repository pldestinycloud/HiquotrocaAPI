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
        public async Task<IActionResult> GetUsers()
        {
            var result = await _service.GetAllUsersAsync();

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.Data == null || !result.Data!.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var result = await _service.GetUserByIdAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.Data == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody]  UpdateUserRequest updateUserRequest)
        {
            var result = await _service.UpdateUserAsync(id, new UserDto());

            if (!result.isSuccess)
                return BadRequest(result);

            if (result.Data == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _service.DeleteUserAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{userId:long}/favorites/{postId:long}")]
        public async Task<IActionResult> AddFavoritePost(long userId, long postId)
        {
            var result = await _service.AddFavoritePostAsync(userId, postId);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{userId:long}/favorites/{postId:long}")]
        public async Task<IActionResult> RemoveFavoritePost(long userId, long postId)
        {
            var result = await _service.RemoveFavoritePostAsync(userId, postId);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{userId:long}/follow/{targetUserId:long}")]
        public async Task<IActionResult> FollowUser(long userId, long targetUserId)
        {
            var result = await _service.FollowUserAsync(userId, targetUserId);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{userId:long}/follow/{targetUserId:long}")]
        public async Task<IActionResult> UnfollowUser(long userId, long targetUserId)
        {
            var result = await _service.UnfollowUserAsync(userId, targetUserId);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
