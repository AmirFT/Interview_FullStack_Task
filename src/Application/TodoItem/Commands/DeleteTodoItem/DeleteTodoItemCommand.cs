using BackEnd.Application.Common.Exceptions;
using BackEnd.Application.Common.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest<int>;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
        .TodoItems
        .Where(x => x.Id == request.Id)
        .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("TodoItems", "Task not found") });
        }

        _context.TodoItems.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}
