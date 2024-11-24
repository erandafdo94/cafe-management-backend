using CafeManagement.Infrastructure.Entities;
using MediatR;

namespace CafeManagement.Application.Dto;

public record CreateEmployeeCommand(
    string Name,
    string EmailAddress,
    string PhoneNumber,
    Gender Gender,
    Guid? CafeId
) : IRequest<string>;