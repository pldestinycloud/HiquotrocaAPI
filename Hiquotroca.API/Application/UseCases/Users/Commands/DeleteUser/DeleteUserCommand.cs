using MediatR;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.DeleteUser;

public record DeleteUserCommand(long Id) : IRequest;
