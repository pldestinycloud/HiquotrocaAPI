using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Users;

namespace Hiquotroca.API.Domain.Entities.Chats
{
    public class Message : BaseEntity
    {
        public long ChatId { get; private set; }
        public long SenderId { get; private set; }
        public long ReceiverId { get; private set; }
        public string Content { get; private set; } = string.Empty;
        public bool IsRead { get; private set; }

        public Message(long chatId, long senderId, long receiverId, string content)
        {
            ChatId = chatId;
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
            IsRead = false;
        }

        public Message MarkAsRead()
        {
            IsRead = true;
            return this;
        }
    }
}

