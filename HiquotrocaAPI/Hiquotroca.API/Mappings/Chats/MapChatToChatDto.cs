using Hiquotroca.API.DTOs.Chat;

namespace Hiquotroca.API.Mappings.Chats;

public static class MapChatToChatDto
{
    public static ChatDto Map(Domain.Entities.Chats.Chat chat, ChatDto chatDto)
    {
        chatDto.Id = chat.Id;
        chatDto.UserId1 = chat.UserId1;
        chatDto.UserId2 = chat.UserId2;
        chatDto.PostId = chat.PostId;

        return chatDto;
    }
}
