using ErrorOr;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Domain.Entities;
using MicroBase.CommandService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.CommandService.Infrastructure.Services;

public class PlatformService(AppDbContext appDbContext) : IPlatformService
{
    public async Task<IEnumerable<Platform>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await appDbContext
            .Platforms
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Platform>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var platform = await appDbContext
            .Platforms
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return platform is not null ? platform : Error.NotFound();
    }

    public async Task<ErrorOr<Platform>> CreateAsync(Platform platform, CancellationToken cancellationToken)
    {
        appDbContext.Platforms.Add(platform);

        return await appDbContext.SaveChangesAsync(cancellationToken) > 0 ? platform : Error.Unexpected();
    }

    public async Task<bool> ExternalPlatformExistsAsync(int externalPlatformId, CancellationToken cancellationToken)
    {
        return await appDbContext
            .Platforms
            .AsNoTracking()
            .AnyAsync(p => p.ExternalId == externalPlatformId, cancellationToken);
    }
}