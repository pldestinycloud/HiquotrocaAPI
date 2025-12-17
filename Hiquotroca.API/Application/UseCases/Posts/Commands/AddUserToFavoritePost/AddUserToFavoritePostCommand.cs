using MediatR;

namespace Hiquotroca.API.Application.UseCases.Posts.Commands.AddUserToFavoritePost;

public record AddUserToFavoritePostCommand(long PostId, long UserId) : IRequest;
