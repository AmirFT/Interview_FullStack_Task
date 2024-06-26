using BackEnd.Application.Common.Mappings;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;

namespace BackEnd.Application.TodoItems.Queries.Get;

public class TodoItemDto : IMapFrom<TodoItem>
{
    public int Status { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public PriorityLevel Priority { get; set; }
    public TodoItemEmployeeBriefDto Employee { get; set; }
}
