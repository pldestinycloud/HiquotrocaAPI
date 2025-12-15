using MediatR;

namespace Hiquotroca.API.Application.Features.Lotteries.Commands.PurchaseTicket;

public record PurchaseTicketCommand(
    long LotteryId,
    long UserId,
    int SelectedNumber
) : IRequest;