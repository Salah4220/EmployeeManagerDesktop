namespace EmployeeManager.Domain.Entities
{
        public class TaskItem
        {

            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string? State { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Updated { get; set; }
            public int? effortEstimation { get; set; }
            public int? priority { get; set; }
            public int? progress { get; set; }
            public int? UserId { get; set; }
            public User? User { get; set; }
        }

    

}
