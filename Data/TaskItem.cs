using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanChatApp.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; 
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty; 

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!; 

        public string? AssignedUserId { get; set; }

        [ForeignKey("AssignedUserId")]
        public ApplicationUser? AssignedUser { get; set; }
    }

}
