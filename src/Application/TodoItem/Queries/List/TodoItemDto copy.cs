using BackEnd.Application.Common.Mappings;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;

namespace BackEnd.Application.TodoItems.Queries.List;

public class TodoItemDto : IMapFrom<TodoItem>
{
    public int Status { get; set; }
    public List<TodoItemBriefDto> Details { get; set; }
}
