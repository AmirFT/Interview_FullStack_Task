using BackEnd.Application.TodoLists.Queries.ExportTodos;

namespace BackEnd.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
