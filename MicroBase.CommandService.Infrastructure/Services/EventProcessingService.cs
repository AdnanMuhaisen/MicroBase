using ErrorOr;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Domain.Entities;
using MicroBase.CommandService.Domain.Enums;
using MicroBase.CommandService.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MicroBase.CommandService.Infrastructure.Services;

public class EventProcessingService(IServiceScopeFactory serviceScopeFactory, ILogger<EventProcessingService> logger) : IEventProcessingService
{
    public async Task ProcessAsync(string message, CancellationToken cancellationToken)
    {
        var eventType = _GetEventType(message);
        if (eventType is PlatformEvent.Published)
        {
            var result = await _AddPlatformAsync(message, cancellationToken);
            if (result.IsError || !result.Value)
            {
                logger.LogError($"{(result.IsError ? result.FirstError.Description : "An error occured while trying to add platform")} at {DateTime.Now}");
            }
        }
    }

    private PlatformEvent? _GetEventType(string notificationMessage)
    {
        var genericEventType = JsonSerializer.Deserialize<GenericEvent>(notificationMessage);

        return (genericEventType is not null) ? genericEventType.Event : null;
    }

    private async Task<ErrorOr<bool>> _AddPlatformAsync(string platformMessage, CancellationToken cancellationToken = default)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var platformService = scope.ServiceProvider.GetRequiredService<IPlatformService>();
        var publishedPlatform = JsonSerializer.Deserialize<PublishedPlatform>(platformMessage, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (publishedPlatform is null)
        {
            return Error.Validation();
        }

        Platform platform = new()
        {
            Name = publishedPlatform.Name,
            ExternalId = publishedPlatform.Id
        };

        if (!await platformService.ExternalPlatformExistsAsync(publishedPlatform.Id, cancellationToken))
        {
            var result = await platformService.CreateAsync(platform, cancellationToken);
            return (!result.IsError && result.Value.Id != 0);
        }
        else
        {
            Console.WriteLine($"Platform {platformMessage} already exists");

            return Error.Conflict();
        }
    }
}