using Hiquotroca.API.Application.Services;
using Hiquotroca.API.DTOs.Chat;
using Microsoft.AspNetCore.SignalR;

namespace Hiquotroca.API.Presentation.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"User connected: {Context.UserIdentifier}");
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            //_ = Task.Run(() => _chatService.AddMessageAsync(chatId, senderId, receiverId, content));
            Console.WriteLine(message);
            
            //await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", content);
        }
    }
}