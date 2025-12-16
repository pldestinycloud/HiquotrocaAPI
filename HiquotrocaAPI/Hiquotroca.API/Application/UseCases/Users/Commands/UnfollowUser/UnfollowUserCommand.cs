using MediatR;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.UnfollowUser;

public record UnfollowUserCommand(long UserId, long TargetUserId) : IRequest;
