namespace BackEnd.Domain.Entities;

public class Employee : BaseAuditableEntity
{
    public string Name { get; set; }
    public bool IsManager { get; set; }
    public ICollection<TodoItem> TodoItems { get; set; }
}
