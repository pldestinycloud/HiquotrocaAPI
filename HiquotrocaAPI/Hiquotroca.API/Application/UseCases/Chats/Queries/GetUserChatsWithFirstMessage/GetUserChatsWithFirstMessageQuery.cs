using Hiquotroca.API.DTOs.Chat;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.UseCases.Chats.Queries.GetUserChatsWithFirstMessage;

public record GetUserChatsWithFirstMessageQuery(long UserId) : IRequest<List<ChatDto>>;
