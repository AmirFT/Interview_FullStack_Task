using BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Employee> Employees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
