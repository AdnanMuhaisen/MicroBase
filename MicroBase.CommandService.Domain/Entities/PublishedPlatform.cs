using MicroBase.CommandService.Domain.Enums;

namespace MicroBase.CommandService.Domain.Entities;

public class PublishedPlatform
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public PlatformEvent Event { get; set; }
}