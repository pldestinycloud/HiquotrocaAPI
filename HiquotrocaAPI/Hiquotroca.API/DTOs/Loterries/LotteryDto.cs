using Hiquotroca.API.Domain.Entities;

namespace Hiquotroca.API.DTOs.Lotteries;

public class LotteryDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double TicketPrice { get; set; }
    public int TotalTickets { get; set; }
    public int TicketsSold { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public List<int> SoldNumbers { get; set; } = new List<int>();
}

public class TicketDto
{
    public long Id { get; set; }
    public int SelectedNumber { get; set; }
    public DateTime PurchaseDate { get; set; }
    public long UserId { get; set; }
}