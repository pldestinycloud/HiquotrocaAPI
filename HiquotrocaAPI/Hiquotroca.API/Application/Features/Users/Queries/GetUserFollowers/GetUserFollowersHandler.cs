using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserFollowers;

public class GetUserFollowersHandler(AppDbContext db) : IRequestHandler<GetUserFollowersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUserFollowersQuery request, CancellationToken cancellationToken)
    {
        // Supondo que existe uma relação de seguidores na tabela UserFollowing
        var followingUsersIds = new List<int>();/*await db.Set<User>()
            .Where(f => f.UserId == request.UserId)
            .Select(f => f.FollowedId)
            .ToListAsync();*/

        if (followingUsersIds == null || !followingUsersIds.Any())
            return new List<UserDto>();

        var followingUsers = new List<User>(); //await db.Users.Where(u => followingUsersIds.Contains(u.Id)).ToListAsync();

        return followingUsers.Select(user => new UserDto
        {
            Id = user.Id,
            Nome = user.FirstName,
            Sobrenome = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            BirthDate = user.BirthDate,
        }).ToList();
    }
}
