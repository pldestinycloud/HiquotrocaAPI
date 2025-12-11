using MediatR;

namespace Hiquotroca.API.Application.Features.Posts.Commands.RemoveUserFromFavoritePost;

public record RemoveUserFromFavoritePostCommand(long PostId, long UserId) : IRequest<bool>;
