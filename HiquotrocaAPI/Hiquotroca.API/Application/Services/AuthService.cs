using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Users;
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
        /* 
        Vou fazer as chamadas à BD aqui mesmo porque honestamente a bd princinpal e a bd de autenticação deviam ser duas db separadas
        Além disso, o repositorio injectado é uma implementação concreta em vez de ser uma interface, o que não é uma boa prática
        além de que pode gerar problemas de manutenção no futuro derivado ao forte acoplamento entre os serviços.

        ******* PARA UMA VERSAO 2 MUDAR *******
        */

        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, PasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        //Aqui novamente o serviço devia ser unware de como é que os dados chegam à aplicação, porque agora recebe por htttp, mas podia muito bem receber por outro protocolo
        // e o protocolo ser diferente. Por isso o que devia acontecer era o controlador mapear o registerRequest para um DTO genérico que o serviço entendesse. Mas enfim
        public async Task<BaseResult> RegisterUser(RegisterUserDto registerRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerRequest.Email);

            if (user != null)
            {
                return BaseResult.Failure(new List<Error> { new Error(ErrorCode.InvalidAction, "A user with this email already exists.") });
            }

            var newUser = new User(
                email: registerRequest.Email,
                firstName: registerRequest.FirstName,
                lastName: registerRequest.LastName
            );

            var passwordHash = _passwordHasher.HashPassword(newUser, registerRequest.Password);
            newUser.UpdateUserPassword(passwordHash);

            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return BaseResult.Ok();
            }
            catch (Exception ex)
            {
                return BaseResult.Failure(new Error(ErrorCode.Exception, $"An error occurred while creating the user: {ex.Message}"));
            }
        }

        public async Task<BaseResult<object>> LoginUser(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BaseResult<LoginResponse>.Failure(new Error(ErrorCode.ErrorInIdentity, "Invalid email or password."));
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return BaseResult<LoginResponse>.Failure(new Error(ErrorCode.ErrorInIdentity, "Invalid email or password."));
            }

            // Generate JWT token
            var accessToken = GenerateJwtToken(user);

            // Generate Refresh Token
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            user.UpdateRefreshToken(
                refreshToken: refreshToken,
                expiry: DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshTokenDurationInDays")));

            await _context.SaveChangesAsync();

            var response = new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return BaseResult<object>.Ok(response);
        }


        //No futuro mudar para uma classe Independente do estilo TokenProvider or wtv
        private string GenerateJwtToken(User user)
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

        //SE calhar nao devia retornar um LoginResponse mas sim um objecto mais adequado
        //No entanto o LoginResponse ja tem os campos necessarios
        public async Task<BaseResult<object>> GetAccessTokenWithRefreshToken(long userId, string refreshToken)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null
                || user.RefreshTokenExpiry == null
                || user.RefreshTokenExpiry < DateTime.UtcNow
                || user.RefreshToken != refreshToken)
            {
                return BaseResult<object>.Failure(new Error(ErrorCode.AccessDenied, "A valid refresh token needs to be sent to"));
            }

            var newAccessToken = GenerateJwtToken(user);

            return BaseResult<object>.Ok(newAccessToken);
        }
    }
}
