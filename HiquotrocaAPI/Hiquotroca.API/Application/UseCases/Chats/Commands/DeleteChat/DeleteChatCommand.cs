using MediatR;

namespace Hiquotroca.API.Application.UseCases.Chats.Commands.DeleteChat;

public record DeleteChatCommand(long ChatId) : IRequest;
