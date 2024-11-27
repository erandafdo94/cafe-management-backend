using CafeManagement.Application.Dto;
using MediatR;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto>;