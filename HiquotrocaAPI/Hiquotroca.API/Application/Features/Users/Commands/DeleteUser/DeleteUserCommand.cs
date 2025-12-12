using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(long Id) : IRequest;
