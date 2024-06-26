using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Application.Common.Mappings;
using BackEnd.Application.Common.Models;
using BackEnd.Application.PredicateBuilders;
using BackEnd.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.TodoItems.Queries.List;

public record GetTodoItemsQuery : IRequest<List<TodoItemsDto>>
{
    public int UserId { get; init; }
}

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoItemsDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {

        var predicate = PredicateBuilder.True<TodoItem>();

        if (request.UserId != 0)
        {
            predicate = predicate.And(x => x.EmployeeId == request.UserId);
        }

        return await _context.TodoItems
        .Include(x => x.Employee)
            .Where(predicate)
            .GroupBy(x => x.Status)
            .OrderByDescending(x => x.Key)
            .Select(x => new TodoItemsDto
            {
                Status = x.Key,
                Details = x.Select(y => new TodoItemBriefDto
                {
                    Id = y.Id,
                    Description = y.Description,
                    Priority = y.Priority,
                    Title = y.Title,
                    Employee = new TodoItemEmployeeBriefDto
                    {
                        Id = y.Employee.Id,
                        IsManager = y.Employee.IsManager,
                        Name = y.Employee.Name,
                    }
                }).ToList()
            })
            .ToListAsync();
    }
}
