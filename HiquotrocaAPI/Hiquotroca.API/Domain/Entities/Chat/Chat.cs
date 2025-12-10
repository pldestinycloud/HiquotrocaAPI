using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Posts;

namespace Hiquotroca.API.Domain.Entities.Chats
{
    public class Chat : BaseEntity
    {
        public long UserId1 { get; private set; }
        public long UserId2 { get; private set; }
        public long? PostId { get; private set; }
        public List<Message> Messages { get; private set; } = new List<Message>();

        public Chat(long userId1, long userId2, long? postId)
        {
            UserId1 = userId1;
            UserId2 = userId2;
            PostId = postId;
            Messages = new List<Message>();
        }

        public bool AddUser(long userId)
        {
            if (UserId1 == 0 && UserId2 != userId)
            {
                UserId1 = userId;
                return true;
            }
            if (UserId2 == 0 && UserId1 != userId)
            {
                UserId2 = userId;
                return true;
            }
            return false;
        }

        public bool RemoveUser(long userId)
        {
            if (UserId1 == userId)
            {
                UserId1 = 0;
                return true;
            }
            if (UserId2 == userId)
            {
                UserId2 = 0;
                return true;
            }
            return false;
        }

        public void Delete()
        {
            
        }

        public bool HasUser(long userId)
        {
            return UserId1 == userId || UserId2 == userId;
        }

        public Message? GetFirstMessage()
        {
            return Messages?.OrderBy(m => m.Id).FirstOrDefault();
        }

        public List<Message> GetMessages()
        {
            return Messages?.OrderBy(m => m.Id).ToList() ?? new List<Message>();
        }
    }
}
