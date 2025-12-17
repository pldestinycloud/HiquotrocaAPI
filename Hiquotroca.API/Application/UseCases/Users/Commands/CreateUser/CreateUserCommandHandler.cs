using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(AppDbContext context, AuthService authService) : IRequestHandler<CreateUserCommand>
    { 
        private readonly Regex EmailRegex = new(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Email) || !EmailRegex.IsMatch(command.Email))
                throw new InvalidOperationException("Invalid email address.");

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