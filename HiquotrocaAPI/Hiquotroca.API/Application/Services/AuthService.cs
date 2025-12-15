using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Auth;
using Hiquotroca.API.DTOs.Auth.Requests;
using Hiquotroca.API.DTOs.Auth.Responses;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Hiquotroca.API.Application.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext context, PasswordHasher<User> passwordHasher, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _tokenService = tokenService;

        }

        public async Task<object> LoginUser(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return "Invalid email or password.";

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return "Invalid email or password.";

            var accessToken = _tokenService.GenerateJwtToken(user);
            var refreshTokenDto = GenerateRefreshTokenDto();

            user.UpdateRefreshToken(refreshTokenDto.RefreshToken,refreshTokenDto.ExpiryDate);

            await _context.SaveChangesAsync();

            return new
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenDto.RefreshToken,
            };
        }

        public string HashPasswordForUser(User user, string password)
        {
             return _passwordHasher.HashPassword(user, password);
        }

        public async Task<string?> GetAccessTokenWithRefreshToken(long userId, string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null ;

            if(!_tokenService.IsRefreshTokenValid(user, refreshToken))
                return null;

            return _tokenService.GetAccessTokenWithRefreshToken(user!, refreshToken);
        }

        public RefreshTokenDto GenerateRefreshTokenDto()
        {
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expiryDate = _tokenService.CalculateRefreshTokenExpiryDate();

            return new RefreshTokenDto
            {
                RefreshToken = refreshToken,
                ExpiryDate = expiryDate 
            };
        }
    }
}
