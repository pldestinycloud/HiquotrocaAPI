using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.DTOs.Chat;

namespace Hiquotroca.API.Mappings.Chats;

public static class MapMessageToMessageDto
{
    public static MessageDto Map(Message? message, MessageDto messageDto)
    {
        if (message == null)
            return messageDto;

        messageDto.Id = message.Id;
        messageDto.Content = message.Content;
        messageDto.SenderId = message.SenderId;
        messageDto.ReceiverId = message.ReceiverId;
        messageDto.Timestamp = message.CreatedDate;
        messageDto.IsRead = message.IsRead;

        return messageDto;
    }
}
