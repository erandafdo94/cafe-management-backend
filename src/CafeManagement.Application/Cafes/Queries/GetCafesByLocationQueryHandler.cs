using CafeManagement.Application.Dto;
using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Application.Cafes.Queries;

public class GetCafesByLocationQueryHandler : IRequestHandler<GetCafesByLocationQuery, IEnumerable<CafeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCafesByLocationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CafeDto>> Handle(GetCafesByLocationQuery request, CancellationToken cancellationToken)
    {
        var cafes = await _context.Cafes
            .Include(c => c.Employees)
            .Where(c => string.IsNullOrWhiteSpace(request.Location) || 
                        c.Location.ToLower() == request.Location.ToLower())
            .Select(c => new 
            {
                c.Id,
                c.Name,
                c.Description,
                c.Logo,
                c.Location,
                EmployeeCount = c.Employees.Count
            })
            .ToListAsync(cancellationToken);

        return cafes
            .Select(c => new CafeDto(
                c.Id,
                c.Name,
                c.Description,
                c.Logo,
                c.Location,
                c.EmployeeCount
            ))
            .OrderByDescending(c => c.EmployeeCount);
    }
}