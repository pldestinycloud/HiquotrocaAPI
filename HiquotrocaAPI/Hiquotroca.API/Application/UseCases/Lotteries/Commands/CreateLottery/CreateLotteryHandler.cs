using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.Features.Lotteries.Commands.CreateLottery;

public class CreateLotteryHandler(AppDbContext db) : IRequestHandler<CreateLotteryCommand>
{
    public async Task Handle(CreateLotteryCommand request, CancellationToken cancellationToken)
    {
        var lottery = new Lottery(
            request.Title,
            request.Description,
            request.TicketPrice,
            request.TotalTickets,
            request.MinTicketsSold,
            request.ExpiryDate,
            request.ImageUrl
        );

        db.Set<Lottery>().Add(lottery);
        await db.SaveChangesAsync(cancellationToken);
    }
}