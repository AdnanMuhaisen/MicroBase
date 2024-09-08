using ErrorOr;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Domain.Entities;
using MicroBase.CommandService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.CommandService.Infrastructure.Services;

public class CommandService(AppDbContext appDbContext) : ICommandService
{
    public async Task<IEnumerable<Command>> GetPlatformCommandsAsync(int platformId, CancellationToken cancellationToken)
    {
        return await appDbContext
            .Commands
            .AsNoTracking()
            .Where(x => x.PlatformId == platformId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Command>> GetById(int commandId, int platformId, CancellationToken cancellationToken)
    {
        var command = await appDbContext
            .Commands
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == commandId && x.PlatformId == platformId, cancellationToken);

        return command is not null ? command : Error.NotFound("Command.NotFound");
    }

    public async Task<ErrorOr<Command>> CreateAsync(Command command, CancellationToken cancellationToken)
    {
        appDbContext.Add(command);

        return await appDbContext.SaveChangesAsync(cancellationToken) > 0 ? command : Error.Unexpected();
    }
}