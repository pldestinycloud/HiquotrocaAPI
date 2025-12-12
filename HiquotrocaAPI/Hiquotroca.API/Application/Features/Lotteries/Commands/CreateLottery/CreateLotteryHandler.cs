using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.Features.Lotteries.Commands.CreateLottery;

public class CreateLotteryHandler(AppDbContext db) : IRequestHandler<CreateLotteryCommand, long>
{
    public async Task<long> Handle(CreateLotteryCommand request, CancellationToken cancellationToken)
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

        db.Set<Lottery>().Add(lottery); // Assumindo que adicionaste DbSet<Lottery> no AppDbContext
        await db.SaveChangesAsync(cancellationToken);

        return lottery.Id;
    }
}