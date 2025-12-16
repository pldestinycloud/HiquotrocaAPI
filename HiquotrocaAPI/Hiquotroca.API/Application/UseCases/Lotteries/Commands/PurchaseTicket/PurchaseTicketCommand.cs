using MediatR;

namespace Hiquotroca.API.Application.UseCases.Lotteries.Commands.PurchaseTicket;

public record PurchaseTicketCommand(
    long LotteryId,
    long UserId,
    int SelectedNumber
) : IRequest;