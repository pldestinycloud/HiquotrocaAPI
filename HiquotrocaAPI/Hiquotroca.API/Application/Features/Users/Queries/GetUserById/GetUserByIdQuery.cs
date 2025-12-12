using Hiquotroca.API.DTOs.User;
using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(long Id) : IRequest<UserDto?>;
