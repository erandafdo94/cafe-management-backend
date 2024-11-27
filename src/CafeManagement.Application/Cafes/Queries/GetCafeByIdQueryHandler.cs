using CafeManagement.Application.Dto;
using CafeManagement.Application.Interfaces;
using CafeManagement.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Application.Cafes.Queries;

public class GetCafeByIdQueryHandler : IRequestHandler<GetCafeByIdQuery, CafeDto>
{
    private readonly IApplicationDbContext _context;

    public GetCafeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CafeDto> Handle(GetCafeByIdQuery request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (cafe == null)
            throw new NotFoundException(nameof(Cafe), request.Id);

        return new CafeDto
        (
            cafe.Id,
            cafe.Name,
            cafe.Description,
            cafe.Logo,
            cafe.Location,
            cafe.Employees.Count);
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string cafeName, Guid requestId)
    {
        throw new NotImplementedException();
    }
}