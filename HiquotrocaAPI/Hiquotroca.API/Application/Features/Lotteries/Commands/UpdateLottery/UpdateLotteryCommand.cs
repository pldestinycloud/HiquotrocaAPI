using MediatR;

namespace Hiquotroca.API.Application.Features.Lotteries.Commands.UpdateLottery;

public record UpdateLotteryCommand(
    long Id,
    string Title,
    string Description,
    decimal TicketPrice,
    DateTime ExpiryDate,
    string? ImageUrl,
    bool IsActive
) : IRequest;