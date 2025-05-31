using Microsoft.AspNetCore.SignalR;
using KanbanChatApp.Data;
using Microsoft.EntityFrameworkCore;

namespace KanbanChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string projectId, string message)
        {
            if (!int.TryParse(projectId, out int projectIntId)) return;

            var userName = Context.User?.Identity?.Name;
            if (string.IsNullOrEmpty(userName)) return;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return;

            var chatMessage = new ChatMessage
            {
                ProjectId = projectIntId,
                UserId = user.Id,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(projectId).SendAsync("ReceiveMessage", user.UserName, message);
        }

        public override async Task OnConnectedAsync()
        {
            var projectId = Context.GetHttpContext()?.Request.Query["projectId"];
            if (!string.IsNullOrEmpty(projectId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, projectId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var projectId = Context.GetHttpContext()?.Request.Query["projectId"];
            if (!string.IsNullOrEmpty(projectId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
