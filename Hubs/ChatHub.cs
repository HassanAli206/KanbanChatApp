using Microsoft.AspNetCore.SignalR;

namespace KanbanChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string projectId, string user, string message)
        {
            Console.WriteLine($"📨 Hub received message from {user}: {message} in Project {projectId}");
            await Clients.Group(projectId).SendAsync("ReceiveMessage", user, message);
        }



        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var projectId = httpContext?.Request.Query["projectId"];

            Console.WriteLine($"✅ User connected: {Context.ConnectionId}, Project: {projectId}");

            if (!string.IsNullOrEmpty(projectId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, projectId!);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext();
            var projectId = httpContext?.Request.Query["projectId"];

            Console.WriteLine($"⚠️ User disconnected: {Context.ConnectionId}, Project: {projectId}");

            if (!string.IsNullOrEmpty(projectId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId!);
            }

            await base.OnDisconnectedAsync(exception);
        }


    }
}
