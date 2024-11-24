using CafeManagement.Application.Interfaces;
using CafeManagement.Infrastructure.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace CafeManagement.Application.Dto;

public class CreateEmployeeCommandHandler  : IRequestHandler<CreateEmployeeCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateEmployeeCommand> _validator;

    public CreateEmployeeCommandHandler(IApplicationDbContext context, IValidator<CreateEmployeeCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        
        // Check if employee already exists
        if (await _context.Employees.AnyAsync(e => e.EmailAddress == request.EmailAddress, cancellationToken))
        {
            throw new ValidationException("Employee with this email already exists");
        }
        
        // Check if employee can be assigned to cafe
        if (request.CafeId.HasValue)
        {
            var cafeExists = await _context.Cafes
                .AnyAsync(c => c.Id == request.CafeId, cancellationToken);

            if (!cafeExists)
            {
                throw new ValidationException("Specified cafe does not exist");
            }

            // Check if employee already works at another cafe
            var existingEmployment = await _context.Employees
                .AnyAsync(e => e.EmailAddress == request.EmailAddress && e.CafeId != null, 
                    cancellationToken);

            if (existingEmployment)
            {
                throw new ValidationException("Employee already works at another cafe");
            }
        }
        
        // Generate unique employee ID
        string employeeId;
        do
        {
            employeeId = GenerateEmployeeId();
        } while (await _context.Employees.AnyAsync(e => e.Id == employeeId, cancellationToken));

        var employee = new Infrastructure.Entities.Employee
        {
            Id = employeeId,
            Name = request.Name,
            EmailAddress = request.EmailAddress,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            CafeId = request.CafeId,
            StartDate = DateTime.UtcNow
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
    
    private string GenerateEmployeeId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new char[7];
        for (int i = 0; i < 7; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return $"UI{new string(result)}";
    }
}