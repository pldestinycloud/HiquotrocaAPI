using MediatR;

namespace Hiquotroca.API.Application.UseCases.Posts.Commands.DeletePost;

public record DeletePostCommand(long Id) : IRequest;