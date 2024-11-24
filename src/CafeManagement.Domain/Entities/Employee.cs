namespace CafeManagement.Infrastructure.Entities;

public class Employee
{
    public string Id { get; set; } // UIXXXXXXX format
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public Cafe? Cafe { get; set; }
    public Guid? CafeId { get; set; }
    public DateTime StartDate { get; set; } 
}
public enum Gender
{
    Male,
    Female
}
