using Hiquotroca.API.DTOs.Posts;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetAllPosts;

public record GetAllPostsQuery() : IRequest<List<PostDto>>;