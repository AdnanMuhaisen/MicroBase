using MicroBase.CommandService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.CommandService.Api.Extensions;

public static class DatabaseMigrationsManager
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