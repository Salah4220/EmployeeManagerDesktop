using EmployeeManager.Shared;

public interface ITaskRepository
{
    Task<List<EmployeeManager.Shared.Task>> GetAllAsync();
    Task<EmployeeManager.Shared.Task?> GetByIdAsync(int id);
    System.Threading.Tasks.Task AddAsync(EmployeeManager.Shared.Task task);
    System.Threading.Tasks.Task DeleteAsync(EmployeeManager.Shared.Task task);
    System.Threading.Tasks.Task SaveChangesAsync();
}