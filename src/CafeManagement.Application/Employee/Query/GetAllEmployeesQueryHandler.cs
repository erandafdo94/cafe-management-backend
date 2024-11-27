using CafeManagement.Application.Dto;
using CafeManagement.Application.Employee.Query;
using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEmployeesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _context.Employees
            .Include(e => e.Cafe)
            .ToListAsync(cancellationToken);

        var currentDate = DateTime.UtcNow;

        return employees
            .Select(e => new EmployeeDto(
                e.Id,
                e.Name,
                e.EmailAddress,
                e.PhoneNumber,
                (int)(currentDate - e.StartDate).TotalDays,
                e.Cafe?.Name ?? string.Empty
            ))
            .OrderByDescending(e => e.DaysWorked);
    }
}