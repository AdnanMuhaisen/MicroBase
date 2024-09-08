using ErrorOr;
using MicroBase.CommandService.Domain.Entities;

namespace MicroBase.CommandService.Application.Interfaces;

public interface IPlatformService
{
    Task<IEnumerable<Platform>> GetAllAsync(CancellationToken cancellationToken);

    Task<ErrorOr<Platform>> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ErrorOr<Platform>> CreateAsync(Platform platform, CancellationToken cancellationToken);
}