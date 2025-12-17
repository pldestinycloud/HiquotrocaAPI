using MediatR;

namespace Hiquotroca.API.Application.UseCases.Chats.Commands.RemoveUserFromChat;

public record RemoveUserFromChatCommand(long ChatId, long UserId) : IRequest;
