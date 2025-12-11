using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.RemoveFavoritePost;

public record RemoveFavoritePostCommand(long UserId, long PostId) : IRequest;
