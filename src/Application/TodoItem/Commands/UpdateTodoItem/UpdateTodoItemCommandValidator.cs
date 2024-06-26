using BackEnd.Application.Common.Interfaces;
using BackEnd.Application.Common.Validators;
using FluentValidation;

namespace BackEnd.Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandValidator(IApplicationDbContext context)
    {

        _context = context;
        
        RuleFor(v => v.Id)
            .ValidateId();

        RuleFor(v => v.Title)
        .MaxTitleLength();

        RuleFor(v => v.Description)
            .MaxDescriptionLength();

        RuleFor(v => v.AssignedUserId)
            .EmployeeExist(_context);
    }
}
