// Application/Cafes/Queries/GetAllCafesQueryHandler.cs

using CafeManagement.Application.Cafes.Queries;
using CafeManagement.Application.Dto;
using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllCafesQueryHandler : IRequestHandler<GetAllCafesQuery, IEnumerable<CafeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCafesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CafeDto>> Handle(GetAllCafesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Cafes
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Description,
                c.Logo,
                c.Location,
                EmployeeCount = _context.Employees.Count(e => e.CafeId == c.Id)
            })
            .ToListAsync(cancellationToken);

        return result
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