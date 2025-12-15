using MediatR;

namespace Hiquotroca.API.Application.Features.Chats.Commands.AddUserToChat;

public record AddUserToChatCommand(long ChatId, long UserId) : IRequest;
