using MicroBase.PlatformService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.PlatformService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms => Set<Platform>();

    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region PlatformConfiguration
        modelBuilder.Entity<Platform>()
            .ToTable("Platforms")
            .HasKey(t => t.Id);

        modelBuilder.Entity<Platform>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Platform>()
            .Property(t => t.Name)
            .HasMaxLength(200);

        modelBuilder.Entity<Platform>()
            .Property(t => t.Publisher)
            .HasMaxLength(200);
        #endregion
    }
}