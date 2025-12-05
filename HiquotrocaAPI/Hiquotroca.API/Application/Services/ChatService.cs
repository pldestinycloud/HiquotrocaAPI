using Hiquotroca.API.Domain.Entities.Chat;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.DTOs.Chat.Requests;

namespace Hiquotroca.API.Application.Services
{
    public class ChatService
    {
        private readonly ChatRepository _chatRepository;

        public ChatService(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<BaseResult<List<ChatDto>>> GetUserChatsWithFirstMessageAsync(long userId)
        {
            var chats = await _chatRepository.GetChatsByUserIdWithFirstMessageAsync(userId);

            if (chats == null || !chats.Any())
                return BaseResult<List<ChatDto>>.Failure(new Error(ErrorCode.NotFound, "No chats found for the user"));


            var chatDtos = new List<ChatDto>();

            foreach(var chat in chats)
            {
                var firstMessage = chat.GetFirstMessage();

                chatDtos.Add(new ChatDto
                {
                    Id = chat.Id,
                    UserId1 = chat.UserId1,
                    UserId2 = chat.UserId2,
                    PostId = chat.PostId,
                    FirstMessage = firstMessage == null ? null : new MessageDto
                    {
                        Id = firstMessage.Id,
                        ChatId = firstMessage.ChatId,
                        SenderId = firstMessage.SenderId,
                        ReceiverId = firstMessage.ReceiverId,
                        Content = firstMessage.Content,
                        IsRead = firstMessage.IsRead
                    }
                });
            }

            return BaseResult<List<ChatDto>>.Ok(chatDtos);
        }

        public async Task<BaseResult<List<MessageDto>>> GetMessagesByChatIdAsync(long chatId)
        {
            var chat = await _chatRepository.GetChatWithMessagesAsync(chatId);

            if (chat == null)
                return BaseResult<List<MessageDto>>.Failure(new Error(ErrorCode.NotFound, "Chat not found"));
            if(chat.Messages == null || !chat.Messages.Any())
                return BaseResult<List<MessageDto>>.Failure(new Error(ErrorCode.NotFound, "No messages found for the chat"));

            var messages = chat.GetMessages().Select(m => new MessageDto
            {
                Id = m.Id,
                ChatId = m.ChatId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                IsRead = m.IsRead
            }).ToList();

            return BaseResult<List<MessageDto>>.Ok(messages);
        }

        public async Task<BaseResult> AddUserToChatAsync(long chatId, long userId)

        {
            var chat = await _chatRepository.GetByIdAsync(chatId);
            if (chat == null)
                return BaseResult.Failure(new Error(ErrorCode.NotFound, "Chat not found"));
            if (!chat.AddUser(userId))
                return BaseResult.Failure(new Error(ErrorCode.InvalidAction, "Chat already has two users"));

            await _chatRepository.UpdateAsync(chat);
            return BaseResult.Ok();
        }

        public async Task<BaseResult> RemoveUserFromChatAsync(long chatId, long userId)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat == null)
                return BaseResult.Failure(new Error(ErrorCode.NotFound, "Chat not found"));
            if (!chat.RemoveUser(userId))
                return BaseResult.Failure(new Error(ErrorCode.InvalidAction, "User not in chat"));

            await _chatRepository.UpdateAsync(chat);
            return BaseResult.Ok();
        }

        public async Task<BaseResult> DeleteChatAsync(long chatId)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat == null)
                return BaseResult.Failure(new Error(ErrorCode.NotFound, "Chat not found"));

            await _chatRepository.DeleteAsync(chat);
            return BaseResult.Ok();
        }

        public async Task<BaseResult> CreateChatAsync(CreateChatDto dto)
        {
            var chat = new Chat(dto.UserId1, dto.UserId2, dto.PostId);

            await _chatRepository.AddAsync(chat);
            return BaseResult.Ok();
        }
    }
}
