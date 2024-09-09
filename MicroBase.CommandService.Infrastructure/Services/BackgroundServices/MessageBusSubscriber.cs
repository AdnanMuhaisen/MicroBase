using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Infrastructure.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MicroBase.CommandService.Infrastructure.Services.BackgroundServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly RabbitMQSettings _rabbitMQSettings;
    private readonly IEventProcessingService _eventProcessingService;
    private readonly ILogger<MessageBusSubscriber> _logger;
    private IConnection _connection = null!;
    private IModel _channel = null!;
    private string _queueName = null!;

    public MessageBusSubscriber(IEventProcessingService eventProcessingService, IOptions<RabbitMQSettings> options, ILogger<MessageBusSubscriber> logger)
    {
        _eventProcessingService = eventProcessingService;
        _logger = logger;
        _rabbitMQSettings = options.Value;
        InitializeConnection();
    }

    private void InitializeConnection()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMQSettings.Host,
            Port = _rabbitMQSettings.MessageBusPort
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: _rabbitMQSettings.Exchange, type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName, exchange: _rabbitMQSettings.Exchange, routingKey: string.Empty);
        _logger.LogInformation($"Listinig to the message bus at {DateTime.Now}");

        _connection.ConnectionShutdown += (sender, args)
            => Console.WriteLine($"{typeof(MessageBusSubscriber).FullName}: RabbitMQ Connection Shutdown at {DateTime.Now}");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        // start listening
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, args) =>
        {
            _logger.LogInformation($"An event received at {DateTime.Now}");
            var notificationMessage = Encoding.UTF8.GetString(args.Body.ToArray());
            await _eventProcessingService.ProcessAsync(notificationMessage, stoppingToken);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }

        base.Dispose();
    }
}