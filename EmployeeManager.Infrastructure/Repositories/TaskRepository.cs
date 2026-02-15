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

    public async Task<List<EmployeeManager.Shared.Task>> GetAllAsync()
    {
        return await _context.Tasks
           .Include(t => t.User)
           .ToListAsync();
    }

    public async Task<EmployeeManager.Shared.Task?> GetByIdAsync(int id)
    {
 
        return await _context.Tasks.FindAsync(id);
    }

    public async Task AddAsync(EmployeeManager.Shared.Task task)
    {
        await _context.Tasks.AddAsync(task);
    }

    public async Task DeleteAsync(EmployeeManager.Shared.Task task)
    {
        _context.Tasks.Remove(task);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
