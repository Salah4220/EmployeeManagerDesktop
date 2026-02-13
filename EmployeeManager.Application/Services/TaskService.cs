using EmployeeManager.Application.Interfaces;
using EmployeeManager.Shared;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskDto>> GetAllAsync()
    {
        var tasks = await _repository.GetAllAsync();

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            State = t.State,
            UserName = t.User?.UserName
        }).ToList();
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return null;

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            State = task.State
        };
    }

    public async Task<TaskDto> CreateAsync(TaskDto dto)
    {
        var task = new EmployeeManager.Shared.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            State = dto.State,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };

        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync();

        dto.Id = task.Id;
        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return false;

        await _repository.DeleteAsync(task);
        await _repository.SaveChangesAsync();

        return true;
    }
}
