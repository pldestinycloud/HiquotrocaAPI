using MediatR;

namespace Hiquotroca.API.Application.Features.Posts.Commands.DeletePost;

public record DeletePostCommand(long Id) : IRequest;