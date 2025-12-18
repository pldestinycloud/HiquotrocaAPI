namespace Hiquotroca.API.DTOs.Chat.Requests
{
    public class CreateChatDto
    {
        public long UserId1 { get; set; }
        public long UserId2 { get; set; }
        public long? PostId { get; set; }
    }
}