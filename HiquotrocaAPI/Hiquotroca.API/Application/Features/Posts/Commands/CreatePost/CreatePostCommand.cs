using Hiquotroca.API.DTOs.Posts.Requests;
using MediatR;

namespace Hiquotroca.API.Application.Features.Posts.Commands.CreatePost;

public record CreatePostCommand(CreatePostDto CreatePostDto) : IRequest;