using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserHandler(AppDbContext db) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
        if (user == null)
            return;

        user = user.UpdateUser(
            request.UpdateUserDto.FirstName,
            request.UpdateUserDto.LastName,
            request.UpdateUserDto.PhoneNumber,
            request.UpdateUserDto.BirthDate
        );

        if (request.UpdateUserDto.Address != null)
        {
            user = user.SetUserAddress(
                request.UpdateUserDto.Address.Address,
                request.UpdateUserDto.Address.City,
                request.UpdateUserDto.Address.PostalCode,
                request.UpdateUserDto.Address.CountryId
            );
        }

        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}
