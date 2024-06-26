using BackEnd.Application.Common.Exceptions;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;
using BackEnd.Domain.Events;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<int>
{
    public string Title { get; init; }

    public string Description { get; init; }
    public int AssignedUserId { get; init; }
    public PriorityLevel Priority { get; init; } = PriorityLevel.None;
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {

        Employee targetUser = await _context
        .Employees
        .Where(x => x.Id == request.AssignedUserId)
        .FirstOrDefaultAsync();

        if (targetUser == null)
        {
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("TodoItem", "Assigned User not Found") });
        }

        var entity = new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            
            Employee = targetUser,
            Status = 0
        };


        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
