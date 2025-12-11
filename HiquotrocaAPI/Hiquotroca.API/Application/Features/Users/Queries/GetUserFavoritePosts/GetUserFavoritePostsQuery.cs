using Hiquotroca.API.DTOs.Posts;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserFavoritePosts;

public record GetUserFavoritePostsQuery(long UserId) : IRequest<List<PostDto>>;
