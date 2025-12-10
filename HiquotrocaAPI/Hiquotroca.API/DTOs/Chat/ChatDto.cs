using Hiquotroca.API.Domain.Entities.Chats;
using System.Collections.Generic;

namespace Hiquotroca.API.DTOs.Chat
{
    public class ChatDto
    {
        public long Id { get; set; }
        public long UserId1 { get; set; }
        public long UserId2 { get; set; }
        public long? PostId { get; set; }
        public MessageDto? FirstMessage { get; set; }
    }

    public class MessageDto
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}