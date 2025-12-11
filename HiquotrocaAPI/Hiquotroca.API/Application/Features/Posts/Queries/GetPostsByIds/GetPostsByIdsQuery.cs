using Hiquotroca.API.DTOs.Posts;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetPostsByIds;

public record GetPostsByIdsQuery(List<long> PostsId) : IRequest<List<PostDto>>;