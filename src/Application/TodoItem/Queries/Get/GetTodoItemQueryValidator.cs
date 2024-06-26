using BackEnd.Application.Common.Interfaces;
using BackEnd.Application.Common.Validators;
using FluentValidation;

namespace BackEnd.Application.TodoItems.Queries.Get;

public class GetTodoItemsQueryValidator : AbstractValidator<GetTodoItemsQuery>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
        .ValidateId();


    }
}
