using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Posts;

namespace Hiquotroca.API.Domain.Entities.Chat
{
    public class Chat : BaseEntity
    {
        public long UserId1 { get; private set; }
        public long UserId2 { get; private set; }
        public long? PostId { get; private set; } // conversa ligada a um post
        public List<Message> Mensagens { get; private set; } = new List<Message>();

        public Chat(long userId1, long userId2, long? postId)
        {
            UserId1 = userId1;
            UserId2 = userId2;
            PostId = postId;
            Mensagens = new List<Message>();
        }
    }
}
