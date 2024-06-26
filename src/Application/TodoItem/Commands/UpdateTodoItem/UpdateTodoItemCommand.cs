using BackEnd.Application.Common.Exceptions;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest<int>
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int AssignedUserId { get; init; }
    public int Status { get; init; }
    public PriorityLevel Priority { get; init; } = PriorityLevel.None;

}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        TodoItem entity = await _context
        .TodoItems
        .FirstOrDefaultAsync(x => x.Id == request.Id);

        Employee targetUser = await _context
            .Employees
            .Where(x => x.Id == request.AssignedUserId)
            .FirstOrDefaultAsync();


        if (entity == null)
        {
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("TodoItems", "Incorrect Task selected") });
        }

        if (targetUser == null)
        {
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("TodoItems", "selected user not found") });
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Employee = targetUser;
        entity.Priority = request.Priority;
        entity.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}
