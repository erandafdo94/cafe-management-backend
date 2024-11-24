using CafeManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Application.Cafes.Commands.DeleteCafe;

public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteCafeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes
            .Include(c => c.Employees)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (cafe == null) return false;

        // This will cascade delete or set null depending on your configuration
        _context.Cafes.Remove(cafe);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}