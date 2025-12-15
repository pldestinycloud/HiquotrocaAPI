using Hiquotroca.API.DTOs.User;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<List<UserDto>>;
