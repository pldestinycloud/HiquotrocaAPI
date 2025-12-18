using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.DTOs.Users;
using MediatR;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    long userId,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    DateTime? BirthDate,
    double HiquotrocaCredits,
    UserAddress Address) : IRequest;
