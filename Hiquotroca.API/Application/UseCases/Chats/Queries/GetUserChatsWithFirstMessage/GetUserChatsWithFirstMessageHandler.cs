using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Chats;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Chats.Queries.GetUserChatsWithFirstMessage;

public class GetUserChatsWithFirstMessageHandler(AppDbContext db) : IRequestHandler<GetUserChatsWithFirstMessageQuery, List<ChatDto>>
{
    public async Task<List<ChatDto>> Handle(GetUserChatsWithFirstMessageQuery request, CancellationToken cancellationToken)
    {
        var chats = await db.Chats
            .Select(c => new
            {
                Chat = c,
                FirstMessage = c.Messages.OrderByDescending(m => m.Id).FirstOrDefault()
            })
            .Where(c => c.Chat.UserId1 == request.UserId || c.Chat.UserId2 == request.UserId)
            .ToListAsync();

        if (chats == null || !chats.Any())
            return new List<ChatDto>();

        List<ChatDto> chatDtos = new List<ChatDto>();
        foreach (var chat in chats)
        {
            var chatDto = MapChatToChatDto.Map(chat.Chat, new ChatDto());
            chatDto.FirstMessage = chat.FirstMessage != null 
                ? MapMessageToMessageDto.Map(chat.FirstMessage, new MessageDto()) 
                : null;

            chatDtos.Add(chatDto);
        }

        return chatDtos;
    }
}
