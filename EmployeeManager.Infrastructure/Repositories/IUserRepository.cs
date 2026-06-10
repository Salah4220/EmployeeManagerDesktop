using EmployeeManager.Domain.Entities;
using EmployeeManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<User?> GetByUserNameAsync(string userName);
}

