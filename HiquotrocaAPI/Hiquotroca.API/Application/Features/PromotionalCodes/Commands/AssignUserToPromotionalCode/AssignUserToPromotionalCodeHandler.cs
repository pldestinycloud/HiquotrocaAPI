using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Users;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.PromotionalCodes.Commands.AssignUserToPromotionalCode;

public class AssignUserToPromotionalCodeHandler(AppDbContext db) : IRequestHandler<AssignUserToPromotionalCodeCommand>
{
    public async Task Handle(AssignUserToPromotionalCodeCommand request, CancellationToken cancellationToken)
    {
        var promoCode = await db.PromotionalCodes
            .Include(pc => pc.Owners)
            .FirstOrDefaultAsync(p => p.Id == request.PromoCodeId);
        if (promoCode == null)
            throw new KeyNotFoundException("Promotional code not found.");

        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        promoCode.AssignUser(user);

        db.PromotionalCodes.Update(promoCode);
        await db.SaveChangesAsync();
    }
}
