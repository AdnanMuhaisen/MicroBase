namespace MicroBase.CommandService.Application.Interfaces;

public interface IEventProcessingService
{
    Task ProcessAsync(string message, CancellationToken cancellationToken);
}