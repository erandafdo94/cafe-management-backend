using CafeManagement.Infrastructure.Entities;
using MediatR;

namespace CafeManagement.Application.Employee.Command.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest<bool>
{
    public string Id { get; init; } // Will be set in controller
    public string Name { get; init; }
    public string EmailAddress { get; init; }
    public string PhoneNumber { get; init; }
    public Gender Gender { get; init; }
    public Guid? CafeId { get; init; }
}