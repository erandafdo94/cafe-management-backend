using CafeManagement.Application.Dto;
using MediatR;

namespace CafeManagement.Application.Employee.Query;

public record GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>;
