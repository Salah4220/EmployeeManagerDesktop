using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Shared
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserDTO userDto { get; set; }

    }
        public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
