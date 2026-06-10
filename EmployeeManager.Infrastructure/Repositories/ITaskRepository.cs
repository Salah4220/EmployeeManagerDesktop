
using EmployeeManager.Domain.Entities;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    System.Threading.Tasks.Task AddAsync(TaskItem task);
    System.Threading.Tasks.Task DeleteAsync(TaskItem task);
    System.Threading.Tasks.Task SaveChangesAsync();
}