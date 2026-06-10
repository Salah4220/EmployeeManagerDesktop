using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EmployeeManager.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public string Role { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();


    }
}
