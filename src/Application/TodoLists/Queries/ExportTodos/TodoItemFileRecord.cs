using BackEnd.Application.Common.Mappings;
using BackEnd.Domain.Entities;

namespace BackEnd.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
