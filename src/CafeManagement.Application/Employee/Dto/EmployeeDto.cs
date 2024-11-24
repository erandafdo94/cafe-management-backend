namespace CafeManagement.Application.Dto;

public record EmployeeDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string EmailAddress { get; init; }
    public string PhoneNumber { get; init; }
    public int DaysWorked { get; init; }
    public string? CafeName { get; init; }
}