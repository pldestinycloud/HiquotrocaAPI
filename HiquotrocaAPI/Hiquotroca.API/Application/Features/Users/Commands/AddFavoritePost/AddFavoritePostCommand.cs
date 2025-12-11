using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.AddFavoritePost;

public record AddFavoritePostCommand(long UserId, long PostId) : IRequest;
