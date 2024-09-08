using MicroBase.CommandService.Domain.Entities.Abstractions;

namespace MicroBase.CommandService.Domain.Entities;

public class Command : IEntity
{
    public int Id { get; set; }

    public string Activity { get; set; } = null!;

    public string CommandLine { get; set; } = null!;

    public int PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;
}