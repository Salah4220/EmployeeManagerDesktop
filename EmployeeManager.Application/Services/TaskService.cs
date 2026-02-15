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

    public async Task<TaskCreateUpdateDto> CreateAsync(TaskCreateUpdateDto dto)
    {
        var task = new EmployeeManager.Shared.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            State = dto.State,
            effortEstimation = dto.EffortEstimation,
            priority = dto.Priority,
            progress = dto.Progress,
              
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };



        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync();

       
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, TaskCreateUpdateDto dto)
    {
        var existingTask = await _repository.GetByIdAsync(id);
        if (existingTask == null)
            return false;

        existingTask.Title = dto.Title;
        existingTask.Description = dto.Description;
        existingTask.State = dto.State;
        existingTask.Updated = DateTime.UtcNow;
        existingTask.effortEstimation = dto.EffortEstimation;
        existingTask.priority = dto.Priority ;
        existingTask.progress = dto.Progress;
        await _repository.SaveChangesAsync();
        return true;

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
