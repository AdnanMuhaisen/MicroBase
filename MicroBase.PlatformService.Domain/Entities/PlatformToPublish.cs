using MicroBase.PlatformService.Domain.Entities.Abstractions;
using MicroBase.PlatformService.Domain.Enums;

namespace MicroBase.PlatformService.Domain.Entities;

public class PlatformToPublish : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public PlatformEvent Event { get; set; }
}