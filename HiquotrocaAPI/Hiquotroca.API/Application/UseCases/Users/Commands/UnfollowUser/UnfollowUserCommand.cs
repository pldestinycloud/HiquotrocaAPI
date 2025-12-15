using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.UnfollowUser;

public record UnfollowUserCommand(long UserId, long TargetUserId) : IRequest;
