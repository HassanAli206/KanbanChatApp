using Microsoft.AspNetCore.SignalR;
using KanbanChatApp.Data;
using System;
using System.Threading.Tasks;

namespace KanbanChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string projectId, string user, string message)
        {
            // Save message to database
            var chatMessage = new ChatMessage
            {
                ProjectId = projectId,
                User = user,
                Message = message,
                Timestamp = DateTime.Now
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Send message to the group (clients with same projectId)
            await Clients.Group(projectId).SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var projectId = httpContext?.Request.Query["projectId"];

            Console.WriteLine($"✅ CONNECT: {Context.ConnectionId}, project: {projectId}");

            if (!string.IsNullOrEmpty(projectId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, projectId!);
                Console.WriteLine($"🟢 Added to group: {projectId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext();
            var projectId = httpContext?.Request.Query["projectId"];

            Console.WriteLine($"⚠️ DISCONNECT: {Context.ConnectionId}, project: {projectId}");

            if (!string.IsNullOrEmpty(projectId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId!);
                Console.WriteLine($"🔴 Removed from group: {projectId}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
