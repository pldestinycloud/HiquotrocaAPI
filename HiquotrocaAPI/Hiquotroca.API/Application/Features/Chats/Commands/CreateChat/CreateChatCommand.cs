using Hiquotroca.API.DTOs.Chat.Requests;
using MediatR;

namespace Hiquotroca.API.Application.Features.Chats.Commands.CreateChat;

public record CreateChatCommand(
    long userId1,
    long userId2,
    long postId) : IRequest;
