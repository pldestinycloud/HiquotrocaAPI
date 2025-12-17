using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.UseCases.Payments.Commands.RefillHiquo;

public class RefillHiquoHandler(AppDbContext db) : IRequestHandler<RefillHiquoCommand>
{
    public async Task Handle(RefillHiquoCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with Id {request.UserId} not found.");

        double amountToAdd = request.Quantity;

        if (!string.IsNullOrWhiteSpace(request.PromoCode))
        {
            var promo = await db.PromotionalCodes
                .Include(p => p.Owners)
                .FirstOrDefaultAsync(p => p.Code == request.PromoCode, cancellationToken);

            if (promo == null || !promo.IsValid())
                throw new InvalidOperationException("Promotional code is invalid or expired.");

            if(promo.HasAlreadyBeenUsedByUser(user))
                throw new InvalidOperationException($"Promotional code {promo.Code} was already used by {user.Id}");

            var bonus = promo.CalculateBonus(amountToAdd);
            amountToAdd += bonus;

            promo.AssignUser(user);
            db.PromotionalCodes.Update(promo);
        }

        user.AddCredits(amountToAdd);

        db.Users.Update(user);
        await db.SaveChangesAsync(cancellationToken);
    }
}