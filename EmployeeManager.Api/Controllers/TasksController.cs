using EmployeeManager.Application.Interfaces;
using EmployeeManager.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;


namespace EmployeeManager.Api.Controllers
{


    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateUpdateDto dto)
        {
            var createdTask = await _taskService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskCreateUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("Task data is required.");

            var updated = await _taskService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            




            var task =  await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            await _taskService.DeleteAsync(id);
     

            return NoContent();
        }
        [HttpPut("{id}/assign")]
        public async Task<IActionResult> AssignTaskToUser(int id, [FromBody] AssignTaskDto dto)
        {
            var success = await _taskService.AssignTaskToUserAsync(id, dto.UserId);
            
            if (!success)
                return NotFound(new
                {
                    Success = false,
                    Message = "Tâche ou utilisateur introuvable."
                });

            return Ok(new
            {
                Success = true,
                Message = "Tâche assignée avec succès."
            });
        }








    }

}
