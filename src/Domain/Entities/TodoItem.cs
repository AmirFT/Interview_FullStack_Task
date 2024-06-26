namespace BackEnd.Domain.Entities;

public class TodoItem : BaseAuditableEntity
{
    
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    
}
