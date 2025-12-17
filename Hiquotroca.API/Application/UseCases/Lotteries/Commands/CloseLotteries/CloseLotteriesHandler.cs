using Hiquotroca.API.Application.Interfaces;
using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.Infrastructure.Email;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.Lotteries.Commands.CloseLottery;

public class CloseLotteriesHandler(AppDbContext db, IEmailSender emailSender) : IRequestHandler<CloseLotteriesCommand>
{
    public async Task Handle(CloseLotteriesCommand request, CancellationToken cancellationToken)
    {
        var lotteries = db.Lotteries.Where(l => request.Ids.Contains(l.Id)).ToList();
        if (!lotteries.Any())
            await Task.CompletedTask;

        foreach (var lottery in lotteries)
        {

            try
            {
                lottery.DeactivateLottery();
                var winningTicket = lottery.SetWinner().GetWinnerTicket();

                if (winningTicket is null)
                    continue;

                var winningUserData = db.Users
                    .Where(u => u.Id == winningTicket.UserId)
                    .Select(u => new
                    {
                        u.Id,
                        u.Email,
                        u.FirstName,
                    });

                var winningMessage = $@"
                <h1>Congratulations!</h1>
                <p>Your lottery '{lottery.Title}' has ended.</p>
                <p>The winning ticket number is: <strong>{winningTicket.SelectedNumber}</strong></p>
                <p>Thank you for participating!</p>";

                _ = Task.Run(() => emailSender.SendEmailAsync(
                     winningUserData.First().Email,
                     "You've Won the Lottery!",
                     winningMessage));
            }
            catch
            {
                // Log the error (not implemented here for brevity)
                continue;
            }

            db.UpdateRange(lotteries);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
