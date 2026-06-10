using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure;
using EmployeeManager.Shared;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
           .Include(t => t.User)
           .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
 
        return await _context.Tasks.FindAsync(id);
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
    }

    public async Task DeleteAsync(TaskItem task)
    {
        _context.Tasks.Remove(task);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
