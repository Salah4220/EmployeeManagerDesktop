using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public string Role { get; set; }

        public  ICollection<Task> Tasks { get; set; } = new List<Task>();


    }
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UpdateRoleDto
    {
        public string Role { get; set; } = string.Empty;
    }
}
