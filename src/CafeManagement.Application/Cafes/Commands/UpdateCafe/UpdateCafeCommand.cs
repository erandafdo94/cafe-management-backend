using MediatR;

namespace CafeManagement.Application.Dto.UpdateCafe;

public record UpdateCafeCommand : IRequest<bool>
{
    public Guid Id { get; init; } // This will be set in the controller
    public string Name { get; init; }
    public string Description { get; init; }
    public string? Logo { get; init; }
    public string Location { get; init; }
}