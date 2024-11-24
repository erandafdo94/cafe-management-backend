using MediatR;

namespace CafeManagement.Application.Employee.Command.DeleteEmployee;

public record DeleteEmployeeCommand(string Id) : IRequest<bool>;
