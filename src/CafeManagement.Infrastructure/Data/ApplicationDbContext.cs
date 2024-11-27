using CafeManagement.Application.Interfaces;
using CafeManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(9);
        
                entity.HasOne(e => e.Cafe)
                    .WithMany(c => c.Employees)
                    .HasForeignKey(e => e.CafeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Cafe>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
    
}