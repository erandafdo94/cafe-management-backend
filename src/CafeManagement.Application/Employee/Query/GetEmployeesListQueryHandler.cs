using CafeManagement.Application.Dto;
using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Application.Employee.Query;

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

        var currentDate = DateTime.UtcNow;

        return await query
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                DaysWorked = (int)(currentDate - e.StartDate).TotalDays,
                CafeName = e.Cafe != null ? e.Cafe.Name : string.Empty
            })
            .OrderByDescending(e => e.DaysWorked)
            .ToListAsync(cancellationToken);
    }
}