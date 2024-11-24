using CafeManagement.Application.Dto;
using MediatR;

namespace CafeManagement.Application.Employee.Query;

public record GetEmployeesListQuery : IRequest<IEnumerable<EmployeeDto>>
{
    public Guid? CafeId { get; init; }
}