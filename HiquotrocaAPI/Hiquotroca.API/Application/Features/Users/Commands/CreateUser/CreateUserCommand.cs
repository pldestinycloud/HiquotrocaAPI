using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.CreateUser;

    public record CreateUserCommand(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        string PhoneNumber,
        DateTime BirthDate
    ) : IRequest;
