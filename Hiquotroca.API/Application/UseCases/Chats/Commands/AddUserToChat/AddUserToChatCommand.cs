using MediatR;

namespace Hiquotroca.API.Application.UseCases.Chats.Commands.AddUserToChat;

public record AddUserToChatCommand(long ChatId, long UserId) : IRequest;
