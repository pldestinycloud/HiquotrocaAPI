using MediatR;

namespace Hiquotroca.API.Application.UseCases.Lotteries.Commands.UpdateLottery;

public record UpdateLotteryCommand(
    long Id,
    string Title,
    string Description,
    float TicketPrice,
    DateTime ExpiryDate,
    string? ImageUrl,
    bool IsActive
) : IRequest;