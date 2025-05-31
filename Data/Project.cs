using KanbanChatApp.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KanbanChatApp.Data
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CreatedById { get; set; }

        public ApplicationUser? CreatedBy { get; set; }

        public string ProjectCode { get; set; } = "";

        // ✅ Add this property to fix the missing Tasks error
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    }
}
