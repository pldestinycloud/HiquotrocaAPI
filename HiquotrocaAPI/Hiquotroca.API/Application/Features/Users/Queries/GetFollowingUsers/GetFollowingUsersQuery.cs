using Hiquotroca.API.DTOs.User;
using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetFollowingUsers;

 public record GetFollowingUsersQuery(long userId) : IRequest<List<UserDto>>;