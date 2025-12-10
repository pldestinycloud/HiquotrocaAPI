using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Presentation.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly PromotionalCodeService _promotionalCodeService;
        public UsersController(UserService userService, PostService postService, PromotionalCodeService promotionalCodeService)
        {
            _userService = userService;
            _postService = postService;
            _promotionalCodeService = promotionalCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() =>
            Ok(await _userService.GetAllUsersAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUserAsync(id, updateUserDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("{userId:long}/favorite-posts")]
        public async Task<IActionResult> GetFavoritePosts(long userId)
        {
            var favoritePostIds = await _userService.GetUserFavoritePostsAsync(userId);

            if (favoritePostIds == null || !favoritePostIds.Any())
                return NotFound();

            var favoritePosts = await _postService.GetPostsByIdAsync(favoritePostIds.ToList());
            return Ok(favoritePosts);
        }

        [HttpPost("{userId:long}/favorite-posts/{postId:long}")]
        public async Task<IActionResult> AddFavoritePost(long userId, long postId)
        {
            await _userService.AddFavoritePostAsync(userId, postId);
            return NoContent();
        }

        [HttpDelete("{userId:long}/favorite-posts/{postId:long}")]
        public async Task<IActionResult> RemoveFavoritePost(long userId, long postId)
        {
            await _userService.RemoveFavoritePostAsync(userId, postId);
            return NoContent();
        }

        [HttpGet("{userId:long}/followers")]
        public async Task<IActionResult> GetFollowers(long userId)
        {
            var followers = await _userService.GetUserFollowersAsync(userId);

            if(followers == null || !followers.Any())
                return NotFound();

            return Ok(followers);
        }

        [HttpPost("{userId:long}/followers/{targetUserId:long}")]
        public async Task<IActionResult> FollowUser(long userId, long targetUserId)
        {
            await _userService.FollowUserAsync(userId, targetUserId);
            return NoContent();
        }

        [HttpDelete("{userId:long}/followers/{targetUserId:long}")]
        public async Task<IActionResult> UnfollowUser(long userId, long targetUserId)
        {
            await _userService.UnfollowUserAsync(userId, targetUserId);
            return NoContent();
        }

        [HttpGet("{userId:long}/promotional-codes")]
        public async Task<IActionResult> GetPromotionalCodes(long userId)
        {
            var userPromotionalCodesIds = await _userService.GetUserPromotionalCodesAsync(userId);
            if(userPromotionalCodesIds == null || !userPromotionalCodesIds.Any())
                return NotFound();

            var promotionalCodes = await _promotionalCodeService.GetPromotionalCodesByIdsAsync(userPromotionalCodesIds.ToList());


            return Ok(promotionalCodes);
        }

        [HttpPost("{userId:long}/promotional-codes/{promotionalCodeId:long}")]
        public async Task<IActionResult> ApplyPromoCode(long userId, long promotionalCodeId)
        {
            await _userService.AttributePromotionalCode(userId, promotionalCodeId);
            return NoContent();
        }

        [HttpDelete("{userId:long}/promotional-codes/{promotionalCodeId:long}")]
        public async Task<IActionResult> RemovePromoCode(long userId, long promotionalCodeId)
        {
            await _userService.RemovePromotionalCode(userId, promotionalCodeId);
            return NoContent();
        }
    }
}
