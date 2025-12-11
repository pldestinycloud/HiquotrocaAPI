using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Users.Commands.RemovePromotionalCode;

public class RemovePromotionalCodeHandler(AppDbContext db) : IRequestHandler<RemovePromotionalCodeCommand>
{
    public async Task Handle(RemovePromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return;

        //user.RemovePromotionalCode(request.PromoCodeId);
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}
