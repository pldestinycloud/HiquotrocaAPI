using Hiquotroca.API.DTOs.Posts;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.UseCases.Posts.Queries.GetAllPosts;

public record GetAllPostsQuery() : IRequest<List<PostDto>>;