using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.AssignUserToPromotionalCode;

public class AssignUserToPromotionalCodeHandler(AppDbContext db) : IRequestHandler<AssignUserToPromotionalCodeCommand, bool>
{
    public async Task<bool> Handle(AssignUserToPromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var promoCode = await db.PromotionalCodes.FirstOrDefaultAsync(p => p.Id == request.PromoCodeId);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (promoCode == null || user == null)
            return false;

        // Assign promo code to user (update join table or similar logic)
        // Example: db.UserPromoCodes.Add(new UserPromoCode { UserId = request.UserId, PromoCodeId = request.PromoCodeId });
        await db.SaveChangesAsync();
        return true;
    }
}
