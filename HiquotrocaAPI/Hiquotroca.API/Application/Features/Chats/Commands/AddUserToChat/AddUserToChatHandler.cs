using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Commands.AddUserToChat;

public class AddUserToChatHandler(AppDbContext db) : IRequestHandler<AddUserToChatCommand>
{
    public async Task Handle(AddUserToChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await db.Chats.FirstOrDefaultAsync(c => c.Id == request.ChatId);
        if (chat == null)
            throw new KeyNotFoundException("Chat not found.");

        chat.AddUser(request.UserId);

        db.Chats.Update(chat);
        await db.SaveChangesAsync();
    }
}
