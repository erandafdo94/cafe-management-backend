using CafeManagement.Application.Interfaces;
using CafeManagement.Infrastructure.Entities;
using MediatR;

namespace CafeManagement.Application.Dto;

public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCafeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = new Cafe
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Logo = request.Logo,
            Location = request.Location
        };

        _context.Cafes.Add(cafe);
        await _context.SaveChangesAsync(cancellationToken);

        return cafe.Id;
    }
}