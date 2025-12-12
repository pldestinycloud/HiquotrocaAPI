using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Commands.CreateChat;

public class CreateChatHandler(AppDbContext db) : IRequestHandler<CreateChatCommand>
{
    public async Task Handle(CreateChatCommand command, CancellationToken cancellationToken)
    {
        var chat = new Chat(command.userId1, command.userId2, command.postId);
        await db.Chats.AddAsync(chat);
        await db.SaveChangesAsync();
    }
}
