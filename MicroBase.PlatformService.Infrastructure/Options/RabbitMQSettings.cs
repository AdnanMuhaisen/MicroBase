namespace MicroBase.PlatformService.Infrastructure.Options;

public class RabbitMQSettings
{
    public string Host { get; set; } = null!;

    public int MessageBusPort { get; set; }

    public string Exchange { get; set; } = null!;
}