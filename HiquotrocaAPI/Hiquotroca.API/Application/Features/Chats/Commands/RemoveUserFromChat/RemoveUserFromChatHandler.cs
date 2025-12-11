using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Commands.RemoveUserFromChat;

public class RemoveUserFromChatHandler(AppDbContext db) : IRequestHandler<RemoveUserFromChatCommand, bool>
{
    public async Task<bool> Handle(RemoveUserFromChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == request.ChatId);

        if (chat == null)
            return false;

        if (!chat.RemoveUser(request.UserId))
            return false;

        db.Chats.Update(chat);
        await db.SaveChangesAsync();

        return true;
    }
}
