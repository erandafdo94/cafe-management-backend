using CafeManagement.Application.Cafes.Queries;
using CafeManagement.Application.Dto;
using CafeManagement.Application.Interfaces;
using CafeManagement.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Include(e => e.Cafe)
            .FirstOrDefaultAsync(e => e.Id == request.Id.ToString(), cancellationToken);

        if (employee == null)
            throw new NotFoundException(nameof(Employee), request.Id);

        return new EmployeeDto(
            employee.Id.ToString(),
            employee.Name,
            employee.EmailAddress,
            employee.PhoneNumber,
            (DateTime.Now - employee.StartDate).Days,
            employee.Cafe?.Name ?? string.Empty
        );
    }
}