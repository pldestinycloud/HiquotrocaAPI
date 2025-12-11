using Hiquotroca.API.Application.Services;
using Hiquotroca.API.DTOs.Auth.Requests;
using Hiquotroca.API.DTOs.Users.Requests;
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
        public IActionResult Ping() => Ok("API its working!");

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _authService.LoginUser(request));        
        }

        [HttpPost("get-access-token")]
        public async Task<IActionResult> GetAccessToken(long userId,string refreshToken)
        {
            return Ok();
            //return Ok(await _authService.GetAccessTokenWithRefreshToken(userId, refreshToken));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            return Ok(new { message = "Palavra-passe alterada com sucesso." });
        }
    }
}
