using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Application.Interfaces;
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
using System.Security.Cryptography;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Hiquotroca.API.Application.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public AuthService(AppDbContext context, PasswordHasher<User> passwordHasher, IConfiguration configuration, TokenService tokenService, IEmailSender emailSender)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _tokenService = tokenService;
            _emailSender = emailSender;

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

        // Forgot password: generate token, save on user and send email if user exists.
        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return;

            var tokenInt = RandomNumberGenerator.GetInt32(0, 1_000_000);
            var token = tokenInt.ToString("D6");

            var expiryHours = 2; //Magic Number. Mudar no futuro. Criar uma classe de constantes de configuraçoes ou adicionar uma variavel de ambiente ou assim 
            var expiry = DateTime.UtcNow.AddHours(expiryHours);

            user.SetPasswordResetToken(token, expiry);
            await _context.SaveChangesAsync();

            var subject = "Hiquotroca - Reset password";
            var body = $"Copy the following code to reset your password: {token}. </br>If you did not request this, please ignore this email.";

            _ = Task.Run(() => _emailSender.SendEmailAsync(user.Email, subject, body));
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return false;

            if (!user.IsProvidedPasswordResetTokenValid(request.Token))
                return false;

            var newHash = _passwordHasher.HashPassword(user, request.NewPassword);
            user.UpdateUserPassword(newHash);
            user.ClearPasswordResetToken();

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
