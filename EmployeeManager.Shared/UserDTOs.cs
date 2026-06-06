using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Shared
{

    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
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
