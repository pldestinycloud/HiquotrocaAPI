using MediatR;

namespace Hiquotroca.API.Application.Features.Posts.Commands.AddUserToFavoritePost;

public record AddUserToFavoritePostCommand(long PostId, long UserId) : IRequest<bool>;
