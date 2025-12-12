using Hiquotroca.API.DTOs.Posts;
using MediatR;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetUserFavoritePosts;

public record GetUserFavoritePostsQuery(long UserId) : IRequest<List<PostDto>?>;
