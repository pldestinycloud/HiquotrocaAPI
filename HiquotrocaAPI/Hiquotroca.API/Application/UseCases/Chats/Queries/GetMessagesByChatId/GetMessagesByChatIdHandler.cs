using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Mappings.Chats;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.UseCases.Chats.Queries.GetMessagesByChatId;

public class GetMessagesByChatIdHandler(AppDbContext db) : IRequestHandler<GetMessagesByChatIdQuery, List<MessageDto>>
{
    public async Task<List<MessageDto>> Handle(GetMessagesByChatIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await db.Chats
            .Include(c => c.Messages)
            .Where(c => c.Id == request.ChatId)
            .FirstOrDefaultAsync();

        var messages = chat?.GetMessages();

        if (messages is null || !messages.Any())
            return new List<MessageDto>();

        messages.ForEach(m => m.MarkAsRead());

        db.UpdateRange(messages);
        await db.SaveChangesAsync();

        return messages.Select(m => MapMessageToMessageDto.Map(m, new MessageDto())).ToList();
    }
}
