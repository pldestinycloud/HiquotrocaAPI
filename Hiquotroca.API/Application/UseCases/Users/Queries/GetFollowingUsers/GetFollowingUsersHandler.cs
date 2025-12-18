using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.Users;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.UseCases.Users.Queries.GetFollowingUsers;

public class GetFollowingUsersHandler(AppDbContext db): IRequestHandler<GetFollowingUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetFollowingUsersQuery query, CancellationToken cancellationToken)
    {
        var user = await db.Users
                   .Include(u => u.FollowingUsers)
                   .Where(u => u.Id == query.userId).ToListAsync<User>();

        var followedUsers = user.SelectMany(u => u.FollowingUsers).ToList();

        return followedUsers.Select(user => UserMappers.MapToUserDto(user, new UserDto())).ToList();
    }
}