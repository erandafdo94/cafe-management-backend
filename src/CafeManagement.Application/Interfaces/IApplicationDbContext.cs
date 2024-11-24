using CafeManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Infrastructure.Entities.Employee> Employees { get; }
    DbSet<Cafe> Cafes { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}