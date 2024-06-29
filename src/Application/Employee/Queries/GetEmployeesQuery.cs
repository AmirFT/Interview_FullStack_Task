using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackEnd.Application.Common.Interfaces;
using BackEnd.Application.Common.Mappings;
using BackEnd.Application.Common.Models;
using BackEnd.Application.PredicateBuilders;
using BackEnd.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.Employees.Queries.List;

public record GetEmployeesQuery : IRequest<List<EmployeeDTO>>
{
}

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {

        return await _context
            .Employees.Select(x=>new EmployeeDTO{
                Id = x.Id,
                Name = x.Name,
                IsManager = x.IsManager

            }).ToListAsync();
    }
}
