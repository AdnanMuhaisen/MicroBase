using MicroBase.PlatformService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.PlatformService.Api.Extensions;

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
}