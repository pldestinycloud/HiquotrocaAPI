using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hiquotroca.API.Application.Features.Chats.Commands.CreateChat;

public class CreateChatHandler(AppDbContext db) : IRequestHandler<CreateChatCommand, long?>
{
    public async Task<long?> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = new Chat(request.Dto.UserId1, request.Dto.UserId2, request.Dto.PostId);
        await db.Chats.AddAsync(chat);
        await db.SaveChangesAsync();
        return chat.Id;
    }
}
