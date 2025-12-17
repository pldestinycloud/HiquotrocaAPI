using Hiquotroca.API.DTOs.User;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.Users.Queries.GetUserById;

public record GetUserByIdQuery(long Id) : IRequest<UserDto?>;
