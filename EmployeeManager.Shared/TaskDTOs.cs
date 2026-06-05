using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Shared
{
    
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? State { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? EffortEstimation { get; set; }
        public int? Priority { get; set; }
        public int? Progress { get; set; }
        public string? UserName { get; set; }
        public int? UserId { get; set; }
    }

    public class TaskCreateUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? State { get; set; }
        public int? EffortEstimation { get; set; }
        public int? Priority { get; set; }
        public int? Progress { get; set; }
    }
    public class AssignTaskDto
    {
        public int? UserId { get; set; }
    }

}
