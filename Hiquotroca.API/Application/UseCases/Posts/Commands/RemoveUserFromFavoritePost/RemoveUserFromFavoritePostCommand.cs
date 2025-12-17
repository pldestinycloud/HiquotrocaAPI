using MediatR;

namespace Hiquotroca.API.Application.UseCases.Posts.Commands.RemoveUserFromFavoritePost;

public record RemoveUserFromFavoritePostCommand(long PostId, long UserId) : IRequest;
