using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetFollowingUsers;

public class GetFollowingUsersHandler(AppDbContext db): IRequestHandler<GetFollowingUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetFollowingUsersQuery query, CancellationToken cancellationToken)
    {
        var user = await db.Users
                   .Include(u => u.FollowingUsers)
                   .Where(u => u.Id == query.userId).ToListAsync<User>();

        var followedUsers = user.SelectMany(u => u.FollowingUsers).ToList();

        return followedUsers.Select(user => MapUserToUserDto.Map(user, new UserDto())).ToList();
    }
}