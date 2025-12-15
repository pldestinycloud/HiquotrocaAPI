using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.Lotteries.Commands.CloseLottery;

public class CloseLotteriesHandler(AppDbContext db) : IRequestHandler<CloseLotteriesCommand>
{
    public async Task Handle(CloseLotteriesCommand request, CancellationToken cancellationToken)
    {
        var lotteries = db.Lotteries.Where(l => request.Ids.Contains(l.Id)).ToList();
        if (!lotteries.Any())
            await Task.CompletedTask;

        foreach (var lottery in lotteries)
        {
            lottery.DeactivateLottery();
            int winningTicketNumber = lottery.SetWinnerNumber().GetWinnerNumber();

            // Aditional logic for notifying users
        }

        db.UpdateRange(lotteries);
        await db.SaveChangesAsync(cancellationToken);
    }
}
