using MediatR;

namespace CafeManagement.Application.Cafes.Commands.DeleteCafe;

public record DeleteCafeCommand(Guid Id) : IRequest<bool>;
