using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.Lotteries.Commands.PurchaseTicket;

public class PurchaseTicketHandler(AppDbContext db) : IRequestHandler<PurchaseTicketCommand>
{
    public async Task Handle(PurchaseTicketCommand request, CancellationToken cancellationToken)
    {
        var lottery = await db.Set<Lottery>()
            .Include(l => l.Tickets)
            .FirstOrDefaultAsync(l => l.Id == request.LotteryId, cancellationToken);

        if (lottery == null)
            throw new KeyNotFoundException("Lottery not found.");

        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        double ticketPrice = lottery.GetTicketPrice();
        if (!user.HaveEnoughCredits(ticketPrice))
            throw new InvalidOperationException("User does not have enough credits to purchase the ticket.");

        lottery.PurchaseTicket(user, request.SelectedNumber);
        user.DeducteCredits(ticketPrice);

        db.Users.Update(user);
        db.Lotteries.Update(lottery);

        await db.SaveChangesAsync(cancellationToken);
    }
}