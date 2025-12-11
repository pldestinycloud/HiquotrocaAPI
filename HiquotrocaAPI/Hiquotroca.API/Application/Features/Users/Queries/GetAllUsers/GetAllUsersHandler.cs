using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(AppDbContext db) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, System.Threading.CancellationToken cancellationToken)
    {
        var users = await db.Users.ToListAsync();
        if (users == null || !users.Any())
            return new List<UserDto>();

        return users.Select(user => new UserDto
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
