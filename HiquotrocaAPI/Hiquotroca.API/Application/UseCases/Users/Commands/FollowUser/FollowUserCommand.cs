using MediatR;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.FollowUser;

public record FollowUserCommand(long UserId, long TargetUserId) : IRequest;
