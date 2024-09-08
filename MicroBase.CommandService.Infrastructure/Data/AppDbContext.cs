using MicroBase.CommandService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.CommandService.Infrastructure.Data;

public class AppDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Platform> Platforms => Set<Platform>();

    public DbSet<Command> Commands => Set<Command>();

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
        #endregion

        #region CommandConfiguration
        modelBuilder.Entity<Command>()
            .ToTable("Commands")
            .HasKey(t => t.Id);

        modelBuilder.Entity<Command>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Command>()
            .HasOne(x => x.Platform)
            .WithMany(x => x.Commands)
            .HasForeignKey(t => t.PlatformId)
            .IsRequired();

        modelBuilder.Entity<Command>()
            .Property(x => x.Activity)
            .HasMaxLength(400);

        modelBuilder.Entity<Command>()
            .Property(x => x.CommandLine)
            .HasMaxLength(400);
        #endregion
    }
}