using MicroBase.PlatformService.Domain.Entities;

namespace MicroBase.PlatformService.Application.Interfaces;

public interface ICommandServiceClient
{
    Task SendPlatformAsync(Platform platform, CancellationToken cancellationToken);
}