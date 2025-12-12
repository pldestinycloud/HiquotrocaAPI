using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Commands.DeleteChat;

public class DeleteChatHandler(AppDbContext db) : IRequestHandler<DeleteChatCommand>
{
    public async Task Handle(DeleteChatCommand command, CancellationToken cancellationToken)
    {
        var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == command.ChatId);

        if (chat == null)
            throw new KeyNotFoundException($"Chat with ID {command.ChatId} not found.");

        db.Chats.Remove(chat);
        await db.SaveChangesAsync();
    }
}
