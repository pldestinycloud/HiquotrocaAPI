using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Hiquotroca.API.Application.Services;

public class TokenService
{

    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateRefreshToken() =>
        Guid.NewGuid().ToString().Replace("-", "");

    public DateTime CalculateRefreshTokenExpiryDate() =>
        DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshTokenDurationInDays"));

    public string GetAccessTokenWithRefreshToken(User user, string providedRefreshToken)
    {
         if(!isRefreshTokenValid(user, providedRefreshToken))
            throw new UnauthorizedAccessException("Invalid refresh token. Proceed to login.");

        return GenerateJwtToken(user);
    }


    public string GenerateJwtToken(User user)
    {
        var secretKey = _configuration.GetValue<string>("Jwt:SecretKey")!;
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                }),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:DurationInMinutes")),
            Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
            Audience = _configuration.GetValue<string>("Jwt:Audience"),
            SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(token);
    }

    private bool isRefreshTokenValid(User user, string providedRefreshToken)
    {
        return user != null
            && user.RefreshTokenExpiry != null
            && user.RefreshTokenExpiry >= DateTime.UtcNow
            && user.RefreshToken == providedRefreshToken;
    }
}
