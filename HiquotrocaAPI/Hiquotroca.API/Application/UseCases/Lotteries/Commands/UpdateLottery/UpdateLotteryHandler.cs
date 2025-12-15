using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hiquotroca.API.Application.Features.Lotteries.Commands.UpdateLottery;

public class UpdateLotteryHandler(AppDbContext db) : IRequestHandler<UpdateLotteryCommand>
{
    public async Task Handle(UpdateLotteryCommand request, CancellationToken cancellationToken)
    {
        var lottery = await db.Set<Lottery>()
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

        if (lottery == null)
            throw new KeyNotFoundException($"Lottery with ID {request.Id} not found.");

        lottery.Update(
            request.Title,
            request.Description,
            request.TicketPrice,
            request.ExpiryDate,
            request.ImageUrl,
            request.IsActive
        );

        db.Set<Lottery>().Update(lottery);
        await db.SaveChangesAsync(cancellationToken);
    }
}