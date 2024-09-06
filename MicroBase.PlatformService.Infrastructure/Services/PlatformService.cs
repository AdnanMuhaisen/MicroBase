using ErrorOr;
using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Domain.Entities;
using MicroBase.PlatformService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.PlatformService.Infrastructure.Services;

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

        return platform is not null ? platform : Error.NotFound("Platform.NotFound");   
    }

    public async Task<ErrorOr<Platform>> CreateAsync(Platform platform, CancellationToken cancellationToken)
    {
        await appDbContext.Platforms.AddAsync(platform, cancellationToken);

        return (await appDbContext.SaveChangesAsync(cancellationToken) > 0) ? platform : Error.Unexpected();
    }
}