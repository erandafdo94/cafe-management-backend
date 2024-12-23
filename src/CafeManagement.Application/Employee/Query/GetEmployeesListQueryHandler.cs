using CafeManagement.Application.Dto;
using CafeManagement.Application.Employee.Query;
using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, IEnumerable<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeesListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeDto>> Handle(
        GetEmployeesListQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _context.Employees
            .Include(e => e.Cafe)
            .AsQueryable();

        if (request.CafeId.HasValue)
        {
            query = query.Where(e => e.CafeId == request.CafeId);
        }

        var employees = await query.ToListAsync(cancellationToken);
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