using Hiquotroca.API.Domain.Entities;
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
            throw new KeyNotFoundException("Lotaria não encontrada.");

        if (!lottery.IsActive || lottery.ExpiryDate < DateTime.UtcNow)
            throw new InvalidOperationException("Esta lotaria já não está ativa ou expirou.");

        if (lottery.TicketsSold >= lottery.TotalTickets)
            throw new InvalidOperationException("Todos os bilhetes já foram vendidos.");

        bool numberTaken = lottery.Tickets.Any(t => t.SelectedNumber == request.SelectedNumber);
        if (numberTaken)
            throw new InvalidOperationException($"O número {request.SelectedNumber} já foi comprado.");

        if (request.SelectedNumber < 1 || request.SelectedNumber > lottery.TotalTickets)
            throw new InvalidOperationException($"Número inválido. Deve ser entre 1 e {lottery.TotalTickets}.");

        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException("Utilizador não encontrado.");

        double price = (double)lottery.TicketPrice;

        if (user.HiquoCredits < price)
            throw new InvalidOperationException("Saldo insuficiente de HiquoCredits.");

        user.HiquoCredits -= price; 

        var ticket = new Ticket((int)request.LotteryId, request.UserId, request.SelectedNumber);

        lottery.RegisterTicketSale(ticket);

        db.Users.Update(user);
        db.Set<Lottery>().Update(lottery); 

        await db.SaveChangesAsync(cancellationToken);
    }
}