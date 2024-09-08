using ErrorOr;
using MicroBase.CommandService.Domain.Entities;

namespace MicroBase.CommandService.Application.Interfaces;

public interface ICommandService
{
    Task<IEnumerable<Command>> GetPlatformCommandsAsync(int platformId, CancellationToken cancellationToken);

    Task<ErrorOr<Command>> GetById(int commandId, int platformId, CancellationToken cancellationToken);

    Task<ErrorOr<Command>> CreateAsync(Command command, CancellationToken cancellationToken);
}