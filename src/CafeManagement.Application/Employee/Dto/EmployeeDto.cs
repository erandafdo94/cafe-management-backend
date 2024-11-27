namespace CafeManagement.Application.Dto;

public record EmployeeDto(
    string Id,
    string Name,
    string EmailAddress,
    string PhoneNumber,
    int DaysWorked,
    string CafeName
);