using Hiquotroca.API.Application.Interfaces;
using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Auth.Requests;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Ping() => Ok("API está a funcionar!");

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterUser(request);
            if (!result.isSuccess)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginUser(request);
            if (!result.isSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }

        [HttpPost("get-access-token")]
        public async Task<IActionResult> GetAccessToken(long userId,string refreshToken)
        {
            var result = await _authService.GetAccessTokenWithRefreshToken(userId, refreshToken);
            if (!result.isSuccess)
                return Forbid();

            return Ok(result);
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
