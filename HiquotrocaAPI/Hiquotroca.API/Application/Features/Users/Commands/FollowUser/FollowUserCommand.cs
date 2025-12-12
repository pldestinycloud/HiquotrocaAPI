using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.FollowUser;

public record FollowUserCommand(long UserId, long TargetUserId) : IRequest;
