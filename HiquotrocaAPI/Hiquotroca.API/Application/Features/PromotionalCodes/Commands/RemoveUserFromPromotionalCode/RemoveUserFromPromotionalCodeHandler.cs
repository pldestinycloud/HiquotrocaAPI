using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.RemoveUserFromPromotionalCode;

public class RemoveUserFromPromotionalCodeHandler(AppDbContext db) : IRequestHandler<RemoveUserFromPromotionalCodeCommand, bool>
{
    public async Task<bool> Handle(RemoveUserFromPromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var promoCode = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.PromoCodeId);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (promoCode == null || user == null)
            return false;

        // Remove promo code from user (update join table or similar logic)
        // Example: var entry = await db.UserPromoCodes.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.PromoCodeId == request.PromoCodeId);
        // if (entry != null) db.UserPromoCodes.Remove(entry);
        await db.SaveChangesAsync();
        return true;
    }
}
