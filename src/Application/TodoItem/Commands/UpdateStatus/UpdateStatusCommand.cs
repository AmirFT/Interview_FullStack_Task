using BackEnd.Application.Common.Exceptions;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateStatusCommand : IRequest<int>
{
    public int Id { get; init; }

    public int Status { get; init; }

}

public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        TodoItem entity = await _context
        .TodoItems
        .FirstOrDefaultAsync(x => x.Id == request.Id);


        if (entity == null)
        {
            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("TodoItems", "Incorrect Task selected") });
        }

        entity.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}
