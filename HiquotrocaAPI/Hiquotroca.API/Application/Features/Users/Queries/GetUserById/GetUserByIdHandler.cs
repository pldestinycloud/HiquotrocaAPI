using Hiquotroca.API.DTOs.User;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdHandler(AppDbContext db) : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
        if (user == null)
            return null;

        return MapUserToUserDto.Map(user, new UserDto());
    }
}
