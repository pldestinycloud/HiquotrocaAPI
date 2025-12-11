using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Commands.DeleteChat;

public class DeleteChatHandler(AppDbContext db) : IRequestHandler<DeleteChatCommand, bool>
{
    public async Task<bool> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == request.ChatId);

        if (chat == null)
            return false;

        db.Chats.Remove(chat);
        await db.SaveChangesAsync();

        return true;
    }
}
