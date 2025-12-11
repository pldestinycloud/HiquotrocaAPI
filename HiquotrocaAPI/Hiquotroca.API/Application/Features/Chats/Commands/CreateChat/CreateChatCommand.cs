using Hiquotroca.API.DTOs.Chat.Requests;
using MediatR;

namespace Hiquotroca.API.Application.Features.Chats.Commands.CreateChat;

public record CreateChatCommand(CreateChatDto Dto) : IRequest<long?>;
