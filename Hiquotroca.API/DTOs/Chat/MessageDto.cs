namespace Hiquotroca.API.DTOs.Chat;

public class MessageDto
{
    public long Id { get; set; }
    public long ChatId { get; set; }
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime Timestamp { get; set; }
}
