using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Domain.Entities;
using MicroBase.PlatformService.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MicroBase.PlatformService.Infrastructure.Services;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly RabbitMQSettings _rabbitMQSettings;
    private readonly IConnection _connection = null!;
    private readonly IModel _channel = null!;
    private readonly ILogger<MessageBusClient> _logger;

    public MessageBusClient(IOptions<RabbitMQSettings> options, ILogger<MessageBusClient> logger)
    {
        _logger = logger;
        _rabbitMQSettings = options.Value;
        var connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMQSettings.Host,
            Port = _rabbitMQSettings.MessageBusPort
        };

        try
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _rabbitMQSettings.Exchange, type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += (sender, args) => Console.WriteLine("RabbitMQ Connection Shutdown");
            _logger.LogInformation($"{typeof(MessageBusClient).FullName} Connected to the message bus");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public void PublishNewPlatform(PlatformToPublish platformToPublish)
    {
        var message = JsonSerializer.Serialize(platformToPublish);
        if (!_connection.IsOpen)
        {
            _logger.LogError("The message bus is disconnected");
            return;
        }

        try
        {
            _SendMessage(message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private void _SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        // ignore the routing key
        try
        {
            _channel.BasicPublish(exchange: _rabbitMQSettings.Exchange, routingKey: string.Empty, basicProperties: null!, body: body);
            _logger.LogInformation($"Message: {message} has been published");
        }
        catch
        {
            throw;
        }
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}