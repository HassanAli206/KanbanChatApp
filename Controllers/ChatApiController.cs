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

        public ChatApiController(ApplicationDbContext context) => _context = context;

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetMessages(string projectId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.ProjectId == projectId)
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    m.User,
                    m.Message,
                    Timestamp = m.Timestamp.ToString("HH:mm:ss dd/MM/yyyy")
                })
                .ToListAsync();

            return Ok(messages);
        }
    }
}
