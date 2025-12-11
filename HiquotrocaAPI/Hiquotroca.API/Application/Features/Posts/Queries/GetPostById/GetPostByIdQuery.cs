using Hiquotroca.API.DTOs.Posts;
using MediatR;

namespace Hiquotroca.API.Application.Features.Posts.Queries.GetPostById;

public record GetPostByIdQuery(long Id) : IRequest<PostDto?>;