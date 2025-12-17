using MediatR;

namespace Hiquotroca.API.Application.UseCases.Lotteries.Commands.CreateLottery;

public record CreateLotteryCommand(
    string Title,
    string Description,
    float TicketPrice,
    int TotalTickets,
    int MinTicketsSold,
    DateTime ExpiryDate,
    string ImageUrl
) : IRequest; 