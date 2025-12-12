using Hiquotroca.API.DTOs.Chat;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.Features.Chats.Queries.GetMessagesByChatId;

public record GetMessagesByChatIdQuery(long ChatId) : IRequest<List<MessageDto>>;
