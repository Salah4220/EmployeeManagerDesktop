using EmployeeManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto?> GetByIdAsync(int id);
        Task<List<TaskDto>> GetAllAsync();
        Task<TaskDto> CreateAsync(TaskDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
