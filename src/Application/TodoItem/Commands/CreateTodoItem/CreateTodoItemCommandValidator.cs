using BackEnd.Application.Common.Interfaces;
using BackEnd.Application.Common.Validators;
using FluentValidation;

namespace BackEnd.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{

    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .MaxTitleLength();

        RuleFor(v => v.Description)
            .MaxDescriptionLength();

        RuleFor(v => v.AssignedUserId)
            .EmployeeExist(_context);
    }
}
