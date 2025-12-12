using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Hiquotroca.API.Presentation.Hubs
{
    public class ChatHub(AppDbContext dbContext) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userChatsIds = await dbContext.Chats
                .Where(c => c.UserId1 == Convert.ToInt64(Context.UserIdentifier) ||
                            c.UserId2 == Convert.ToInt64(Context.UserIdentifier))
                .Select(c => c.Id)
                .ToListAsync();

            foreach (var chatId in userChatsIds)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{chatId}");
            }

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(long chatId, long receiverId, long senderId, string message)
        {
            var chatMessage = new Message(chatId, senderId, receiverId, message);
            var saveTask = Task.Run(() => SaveMessageToDatabase(chatId, senderId, receiverId, message));

            await Clients.Group($"chat-{chatId}").SendAsync("ReceiveMessage", chatMessage);

            await saveTask;
        }

        private void SaveMessageToDatabase(long chatId, long senderId, long receiverId, string message)
        {
            var chatMessage = new Message(chatId, senderId, receiverId, message);
            dbContext.Messages.Add(chatMessage);

            dbContext.SaveChanges();
        }
    }
}