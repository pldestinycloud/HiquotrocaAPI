using Hiquotroca.API.DTOs.Users.Requests;
using Hiquotroca.API.DTOs.User;
using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(long Id, UpdateUserDto UpdateUserDto) : IRequest;
