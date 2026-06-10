using EmployeeManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetByIdAsync(int id);
        Task<List<UserDTO>> GetAllAsync();
        Task<RegisterResult> CreateAsync(UserDTO dto);
        Task<bool> UpdateAsync(int id, RegisterDto dto);
        Task<bool> UpdateRole(int id, RoleUpdateDto dto);
        Task<bool> UpdatePassword(int id, PasswordUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<LoginResult> LoginAsync(LoginDto dto);

    }
}
