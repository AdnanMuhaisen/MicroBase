using ErrorOr;
using MicroBase.PlatformService.Domain.Entities;

namespace MicroBase.PlatformService.Application.Interfaces;

public interface IPlatformService
{
    Task<IEnumerable<Platform>> GetAllAsync(CancellationToken cancellationToken);

    Task<ErrorOr<Platform>> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ErrorOr<Platform>> CreateAsync(Platform platform, CancellationToken cancellationToken);
}