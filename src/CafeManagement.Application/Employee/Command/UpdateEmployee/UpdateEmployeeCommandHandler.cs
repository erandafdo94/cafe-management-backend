using System.ComponentModel.DataAnnotations;
using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Application.Employee.Command.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Include(e => e.Cafe)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null) return false;

        // Check if employee can be assigned to new cafe
        if (request.CafeId.HasValue && request.CafeId != employee.CafeId)
        {
            var existingEmployment = await _context.Employees
                .AnyAsync(e => e.EmailAddress == request.EmailAddress 
                               && e.CafeId != null 
                               && e.Id != request.Id, cancellationToken);

            if (existingEmployment)
            {
                throw new ValidationException("Employee already works at another cafe");
            }
        }

        employee.Name = request.Name;
        employee.EmailAddress = request.EmailAddress;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Gender = request.Gender;
        employee.CafeId = request.CafeId;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}