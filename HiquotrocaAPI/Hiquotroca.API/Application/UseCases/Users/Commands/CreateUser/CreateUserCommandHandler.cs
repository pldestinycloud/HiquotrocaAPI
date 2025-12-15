using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(AppDbContext context, AuthService authService) : IRequestHandler<CreateUserCommand>
    { 
        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userExists = await context.Users.AnyAsync(u => u.Email == command.Email);

            if(userExists)
                throw new ApplicationException("User with this email already exists.");

            var user = new User(
                command.Email,
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                command.BirthDate);

            var passwordHash = authService.HashPasswordForUser(user,command.Password);
            user.UpdateUserPassword(passwordHash);

            var refreshTokenDto = authService.GenerateRefreshTokenDto();
            user.UpdateRefreshToken(refreshTokenDto.RefreshToken, refreshTokenDto.ExpiryDate);

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}