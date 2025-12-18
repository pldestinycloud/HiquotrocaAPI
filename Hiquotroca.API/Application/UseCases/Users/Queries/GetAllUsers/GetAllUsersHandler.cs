using Hiquotroca.API.DTOs.Users;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(AppDbContext db) : IRequestHandler<GetAllUsersQuery, List<UserBriefDataDto>>
{
    public async Task<List<UserBriefDataDto>> Handle(GetAllUsersQuery request, System.Threading.CancellationToken cancellationToken)
    {
        var users = await db.Users.ToListAsync();
        if (users == null || !users.Any())
            return new List<UserBriefDataDto>();

        return users.Select(user => UserMappers.MapToUserBriefDataDto(user, new UserBriefDataDto())).ToList();
    }
}
