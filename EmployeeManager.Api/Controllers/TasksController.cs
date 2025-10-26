using EmployeeManager.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = EmployeeManager.Shared.Task;

namespace EmployeeManager.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {

        private readonly HttpClient _client;
        public TasksController(AppDbContext context)
        {
            _context = context;
          
        }
     
        private readonly AppDbContext _context;

        // GET: api/tasks
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.Tasks
         .Include(t => t.User)
         .Select(t => new TaskDto
         {
             Id = t.Id,
             Title = t.Title,
             Description = t.Description,
             State = t.State,
             UserName = t.User.UserName
         })
         .ToListAsync();

            return Ok(tasks);
        }

        // POST: api/tasks
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDto dto) // ✅ FromBody pour Swagger
        {
            if (dto == null)
                return BadRequest("Invalid task data.");

            var task = new Task
            {
                Title = dto.Title,
                Description = dto.Description,
                State = dto.State,
                 UserId = null
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // GET: api/tasks/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // PUT: api/tasks/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
                return NotFound();

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.State = request.State;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {             
                    return NotFound();   
            }
        }

        // DELETE: api/tasks/{id}
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/tasks/{id}/assign/{userId}
        [Authorize(Roles = "admin")]
        [HttpPost("{id}/assign/{userId?}")]
        public async Task<IActionResult> AssignTask(int id, int? userId)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound("Task not found");

            if (userId.HasValue)
            {
                var user = await _context.Users.FindAsync(userId.Value);
                if (user == null)
                    return NotFound("User not found");

                task.UserId = userId.Value;
            }
            else
            {
                task.UserId = null; 
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
