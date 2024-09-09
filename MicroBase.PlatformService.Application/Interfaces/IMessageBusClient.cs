using MicroBase.PlatformService.Domain.Entities;

namespace MicroBase.PlatformService.Application.Interfaces;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformToPublish platformToPublish);
}