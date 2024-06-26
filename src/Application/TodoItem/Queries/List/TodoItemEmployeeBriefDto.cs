using BackEnd.Application.Common.Mappings;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;

namespace BackEnd.Application.TodoItems.Queries.List;

public class TodoItemEmployeeBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsManager { get; set; }
}
