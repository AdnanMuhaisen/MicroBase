using MicroBase.CommandService.Domain.Entities;

namespace MicroBase.CommandService.Application.Interfaces;

public interface IGrpcPlatformDataClient
{
    IEnumerable<Platform> GetAllPlatforms();
}