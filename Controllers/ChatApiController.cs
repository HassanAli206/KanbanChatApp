using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanChatApp.Data;

namespace KanbanChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetMessages(int projectId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.ProjectId == projectId)
                .Include(m => m.User) // 🔥 include user info
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    userName = m.User.UserName,
                    message = m.Message,
                    timestamp = m.Timestamp.ToString("HH:mm:ss dd/MM/yyyy")
                })
                .ToListAsync();

            return Ok(messages);
        }
    }
}
