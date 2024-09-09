using System.Text.Json.Serialization;

namespace MicroBase.PlatformService.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PlatformEvent
{
    Published
}