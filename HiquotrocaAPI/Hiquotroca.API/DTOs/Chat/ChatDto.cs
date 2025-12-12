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
}