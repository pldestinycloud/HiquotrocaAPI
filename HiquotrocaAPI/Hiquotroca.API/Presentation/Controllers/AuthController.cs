using Hiquotroca.API.Application.Services;
using Hiquotroca.API.DTOs.Auth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Hiquotroca.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("API is working!");

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _authService.LoginUser(request));        
        }

        [HttpPost("get-access-token")]
        public async Task<IActionResult> GetAccessToken(long userId,string refreshToken)
        {
            var result = await _authService.GetAccessTokenWithRefreshToken(userId, refreshToken);
            if(result is null)
                return Unauthorized(new { message = "Invalid or expired refresh token." });

            return Ok(new { AccessToken = result });

        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _authService.ForgotPasswordAsync(request.Email);
            // Always return success to avoid user enumeration
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);
            if (!result)
                return BadRequest(new { message = "Invalid or expired token." });

            return Ok(new { message = "Password changed successfully." });
        }
    }
}
