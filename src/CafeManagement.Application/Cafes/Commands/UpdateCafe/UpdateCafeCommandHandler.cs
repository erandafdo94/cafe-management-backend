using CafeManagement.Application.Interfaces;
using MediatR;

namespace CafeManagement.Application.Dto.UpdateCafe;

public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateCafeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (cafe == null) return false;

        cafe.Name = request.Name;
        cafe.Description = request.Description;
        cafe.Logo = request.Logo;
        cafe.Location = request.Location;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}