using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Queries.GetUserChatsWithFirstMessage;

public class GetUserChatsWithFirstMessageHandler(AppDbContext db) : IRequestHandler<GetUserChatsWithFirstMessageQuery, List<ChatDto>>
{
    public async Task<List<ChatDto>> Handle(GetUserChatsWithFirstMessageQuery request, CancellationToken cancellationToken)
    {
        var chats = await db.Chats
            .Include(c => c.Messages)
            .Where(c => c.UserId1 == request.UserId || c.UserId2 == request.UserId)
            .ToListAsync();

        if (chats == null || !chats.Any())
            return new List<ChatDto>();

        var chatDtos = new List<ChatDto>();
        foreach (var chat in chats)
        {
            var firstMessage = chat.GetFirstMessage();
            chatDtos.Add(new ChatDto
            {
                Id = chat.Id,
                UserId1 = chat.UserId1,
                UserId2 = chat.UserId2,
                PostId = chat.PostId,
                FirstMessage = firstMessage == null ? null : new MessageDto
                {
                    Id = firstMessage.Id,
                    ChatId = firstMessage.ChatId,
                    SenderId = firstMessage.SenderId,
                    ReceiverId = firstMessage.ReceiverId,
                    Content = firstMessage.Content,
                    IsRead = firstMessage.IsRead
                }
            });
        }
        return chatDtos;
    }
}
