namespace KanbanChatApp.Data
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string ProjectId { get; set; } = string.Empty; // Default value added
        public string User { get; set; } = string.Empty; // Default value added
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
