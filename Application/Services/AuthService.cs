using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Auth.Requests;
using Hiquotroca.API.DTOs.Auth.Responses;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        private readonly UserRepository _userRepository;
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        public AuthService(UserRepository userService, AppDbContext context, PasswordHasher<User> passwordHasher)
        {
            _userRepository = userService;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        //Aqui novamente o serviço devia ser unware de como é que os dados chegam à aplicação, porque agora recebe por htttp, mas podia muito bem receber por outro protocolo
        // e o protocolo ser diferente. Por isso o que devia acontecer era o controlador mapear o registerRequest para um DTO genérico que o serviço entendesse. Mas enfim
        public async Task<BaseResult> RegisterUser(RegisterRequest registerRequest)
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
                await _userRepository.AddAsync(newUser);
                return BaseResult.Ok();
            }
            catch (Exception ex)
            {
                return BaseResult.Failure(new Error(ErrorCode.Exception, $"An error occurred while creating the user: {ex.Message}"));
            }
        }

        public async Task<BaseResult<LoginResponse>> LoginUser(LoginRequest request)
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
            var accessToken = "Mocked Access Token";
            // Generate Refresh Token
            var refreshToken = "Mocked Refresh Token";

            LoginResponse response = new LoginResponse
            {
                UserId = user.Id,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return BaseResult<LoginResponse>.Ok(response);
        }
    }
}
