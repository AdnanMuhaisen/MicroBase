using System.Text.Json.Serialization;

namespace MicroBase.CommandService.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PlatformEvent
{
    Published
}