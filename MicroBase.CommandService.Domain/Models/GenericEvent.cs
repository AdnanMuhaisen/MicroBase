using MicroBase.CommandService.Domain.Enums;

namespace MicroBase.CommandService.Domain.Models;

public class GenericEvent
{
    public PlatformEvent Event { get; set; }
}