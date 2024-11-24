namespace CafeManagement.Application.Dto;

public record CafeDto(
    Guid Id,
    string Name,
    string Description,
    string? Logo,
    string Location,
    int EmployeeCount
);
