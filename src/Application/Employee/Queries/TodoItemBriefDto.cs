using BackEnd.Application.Common.Mappings;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;


namespace BackEnd.Application.Employees.Queries.List;
public class EmployeeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsManager { get; set; }
}
