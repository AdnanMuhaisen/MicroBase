using MicroBase.CommandService.Domain.Entities.Abstractions;

namespace MicroBase.CommandService.Domain.Entities;

public class Platform : IEntity
{
    public int Id { get; set; }

    public int ExternalId { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Command> Commands { get; set; } = [];
}