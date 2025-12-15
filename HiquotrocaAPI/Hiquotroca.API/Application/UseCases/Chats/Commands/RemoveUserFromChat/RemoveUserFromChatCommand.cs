using MediatR;

namespace Hiquotroca.API.Application.Features.Chats.Commands.RemoveUserFromChat;

public record RemoveUserFromChatCommand(long ChatId, long UserId) : IRequest;
