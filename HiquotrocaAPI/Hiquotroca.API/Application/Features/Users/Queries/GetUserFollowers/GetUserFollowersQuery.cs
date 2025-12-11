using Hiquotroca.API.DTOs.User;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserFollowers;

public record GetUserFollowersQuery(long UserId) : IRequest<List<UserDto>>;
