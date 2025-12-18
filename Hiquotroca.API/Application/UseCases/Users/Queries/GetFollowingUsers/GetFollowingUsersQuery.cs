using Hiquotroca.API.DTOs.Users;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.Users.Queries.GetFollowingUsers;

 public record GetFollowingUsersQuery(long userId) : IRequest<List<UserDto>>;