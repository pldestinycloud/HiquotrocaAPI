using Hiquotroca.API.Domain.Entities.Lottery;
using Hiquotroca.API.DTOs.Lotteries;

namespace Hiquotroca.API.Mappings.Lotteries;

public static class MapLotteryToLotteryDto
{
    public static LotteryDto Map(Lottery lottery)
    {
        return new LotteryDto
        {
            Id = lottery.Id,
            Title = lottery.Title,
            Description = lottery.Description,
            TicketPrice = lottery.TicketPrice,
            TotalTickets = lottery.TotalTickets,
            TicketsSold = lottery.TicketsSold,
            ExpiryDate = lottery.ExpiryDate,
            ImageUrl = lottery.ImageUrl,
            IsActive = lottery.IsActive,
            SoldNumbers = lottery.Tickets?.Select(t => t.SelectedNumber).ToList() ?? new List<int>()
        };
    }
}