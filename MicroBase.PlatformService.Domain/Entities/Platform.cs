using MicroBase.PlatformService.Domain.Entities.Abstractions;

namespace MicroBase.PlatformService.Domain.Entities;

public class Platform : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public decimal Cost { get; set; }
}