using Hiquotroca.API.DTOs.Posts;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.Posts.Queries.GetPostById;

public record GetPostByIdQuery(long Id) : IRequest<PostDto?>;