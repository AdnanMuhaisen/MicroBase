using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.CommandService.Api.Extensions;

public static class DatabaseManager
{
    public static async Task MigrateDatabaseAsync(this WebApplication app, IWebHostEnvironment environment, CancellationToken cancellationToken = default)
    {
        if (environment.IsProduction())
        {
            var serviceScope = app.Services.CreateScope();
            var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            if ((await appDbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                try
                {
                    await appDbContext.Database.MigrateAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public static async Task AddSeedPlatformsAsync(this WebApplication app, CancellationToken cancellationToken)
    {
        using var serviceScope = app.Services.CreateScope();
        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var grpcPlatformDataClient = serviceScope.ServiceProvider.GetRequiredService<IGrpcPlatformDataClient>();
        var platforms = grpcPlatformDataClient.GetAllPlatforms();
        var existingPlatformsIds = await appDbContext
            .Platforms
            .AsNoTracking()
            .Select(platform => platform.ExternalId)
            .ToListAsync(cancellationToken);

        var platformsToAdd = platforms
            .Where(x => !existingPlatformsIds.Contains(x.ExternalId));

        try
        {
            await appDbContext.Platforms.AddRangeAsync(platformsToAdd, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }
}