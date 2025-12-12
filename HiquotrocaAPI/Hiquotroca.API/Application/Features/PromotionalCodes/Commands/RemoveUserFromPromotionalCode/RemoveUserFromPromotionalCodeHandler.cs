using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.RemoveUserFromPromotionalCode;

public class RemoveUserFromPromotionalCodeHandler(AppDbContext db) : IRequestHandler<RemoveUserFromPromotionalCodeCommand>
{
    public async Task Handle(RemoveUserFromPromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var promoCode = await db.PromotionalCodes
             .Include(pc => pc.Owners)
             .FirstOrDefaultAsync(p => p.Id == request.PromoCodeId);
        if (promoCode == null)
            throw new KeyNotFoundException("Promotional code not found.");

        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        promoCode.RemoveUser(user);

        db.PromotionalCodes.Update(promoCode);
        await db.SaveChangesAsync();
    }
}
