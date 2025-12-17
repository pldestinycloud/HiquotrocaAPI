using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserHandler(AppDbContext db) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == command.userId);
        if (user == null)
            return;

        user = user.UpdateUser(
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.BirthDate,
            command.HiquotrocaCredits
        );

        if (command.Address != null)
        {
            user = user.SetUserAddress(
                command.Address.Address,
                command.Address.City,
                command.Address.PostalCode,
                command.Address.CountryId
            );
        }

        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}
