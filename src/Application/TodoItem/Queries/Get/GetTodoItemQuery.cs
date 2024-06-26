using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Application.Common.Mappings;
using BackEnd.Application.Common.Models;
using BackEnd.Application.PredicateBuilders;
using BackEnd.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.TodoItems.Queries.Get;

public record GetTodoItemQuery : IRequest<TodoItemDto>
{
    public int Id { get; init; }
}

public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodoItemDto> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {

        return await _context.TodoItems
            .Include(x => x.Employee)
            .Where(x => x.Id == request.Id)
            .Select(x => new TodoItemDto
            {
                Description = x.Description,
                Id = x.Id,
                Priority = x.Priority,
                Status = x.Status,
                Title = x.Title,
                Employee = new TodoItemEmployeeBriefDto
                {
                    Id = x.Employee.Id,
                    IsManager = x.Employee.IsManager,
                    Name = x.Employee.Name,
                }
            })
            .FirstOrDefaultAsync();
    }
}
