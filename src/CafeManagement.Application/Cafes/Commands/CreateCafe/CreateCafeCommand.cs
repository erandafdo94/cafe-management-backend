using MediatR;

namespace CafeManagement.Application.Dto;

public record CreateCafeCommand(
    string Name,
    string Description,
    string? Logo,
    string Location
) : IRequest<Guid>;