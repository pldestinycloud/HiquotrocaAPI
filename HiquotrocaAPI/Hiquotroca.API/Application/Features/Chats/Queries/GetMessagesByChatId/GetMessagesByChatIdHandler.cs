using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Queries.GetMessagesByChatId;

public class GetMessagesByChatIdHandler(AppDbContext db) : IRequestHandler<GetMessagesByChatIdQuery, List<MessageDto>>
{
    public async Task<List<MessageDto>> Handle(GetMessagesByChatIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await db.Chats
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == request.ChatId);

        if (chat == null || chat.Messages == null || !chat.Messages.Any())
            return new List<MessageDto>();

        return chat.GetMessages().Select(m => new MessageDto
        {
            Id = m.Id,
            ChatId = m.ChatId,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Content = m.Content,
            IsRead = m.IsRead
        }).ToList();
    }
}
