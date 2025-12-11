using MediatR;

namespace Hiquotroca.API.Application.Features.Chats.Commands.DeleteChat;

public record DeleteChatCommand(long ChatId) : IRequest<bool>;
