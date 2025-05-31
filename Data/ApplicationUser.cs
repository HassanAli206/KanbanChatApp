using Microsoft.AspNetCore.Identity;

namespace KanbanChatApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        // ✅ Navigation properties

        public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();
        public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
