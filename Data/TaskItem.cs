using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanChatApp.Data
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "To-Do";

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public string? AssignedUserId { get; set; }

        [ForeignKey("AssignedUserId")]
        public ApplicationUser? AssignedUser { get; set; }
    }
}
