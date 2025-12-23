using Hiquotroca.API.Application.Services;
using Hiquotroca.API.Domain.Entities.Chats;
using Hiquotroca.API.DTOs.Chat;
using Hiquotroca.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq;

namespace Hiquotroca.API.Presentation.Hubs
{
    public class ChatHub(AppDbContext dbContext) : Hub
    {
        public static readonly ConcurrentDictionary<long, HashSet<string>> UserConnections = new();

        public override async Task OnConnectedAsync()
        {
            if (!long.TryParse(Context.UserIdentifier, out var userId))
            {
                await base.OnConnectedAsync();
                return;
            }

            var connections = UserConnections.GetOrAdd(userId, _ => new HashSet<string>());
            connections.Add(Context.ConnectionId);

            var userChatsIds = await dbContext.Chats
                .Where(c => c.UserId1 == userId || c.UserId2 == userId)
                .Select(c => c.Id)
                .ToListAsync();

            foreach (var chatId in userChatsIds)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{chatId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (long.TryParse(Context.UserIdentifier, out var userId))
            {
                if (UserConnections.TryGetValue(userId, out var connections))
                {
                    connections.Remove(Context.ConnectionId);
                    if (connections.Count ==0)
                    {
                        UserConnections.TryRemove(userId, out _);
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Clients can call this to explicitly join a chat group (useful when a chat is created after connection)
        public Task JoinChatGroup(long chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{chatId}");
        }

        public async Task SendMessage(long chatId, long receiverId, long senderId, string message)
        {
            var chatMessage = new Message(chatId, senderId, receiverId, message);
            var saveTask = Task.Run(() => SaveMessageToDatabase(chatId, senderId, receiverId, message));

            await Clients.Group($"chat-{chatId}")
                .SendAsync("ReceiveMessage", chatId, senderId, receiverId, chatMessage);

            await saveTask;
        }

        private void SaveMessageToDatabase(long chatId, long senderId, long receiverId, string message)
        {
            var chatMessage = new Message(chatId, senderId, receiverId, message);
            dbContext.Messages.Add(chatMessage);

            dbContext.SaveChanges();
        }
        public static IEnumerable<string> GetConnections(long userId)
        {
            if (UserConnections.TryGetValue(userId, out var connections))
            {
                return connections.ToList();
            }

            return Enumerable.Empty<string>();
        }
    }
}