using ErrorOr;
using Mapster;
using MicroBase.CommandService.Api.Commands;
using MicroBase.CommandService.Api.Dtos;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroBase.CommandService.Api.Controllers;

[ApiController]
[Route("api/command-service/platforms/{platformId:int}/[controller]")]
public class CommandsController(ICommandService commandService, IPlatformService platformService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByPlatform(int platformId, CancellationToken cancellationToken)
    {
        return (await platformService.GetByIdAsync(platformId, cancellationToken)) == Error.NotFound()
            ? NotFound("Platform not found")
            : Ok((await commandService.GetPlatformCommandsAsync(platformId, cancellationToken)).Adapt<IEnumerable<CommandDto>>());
    }

    [HttpGet("{commandId:int}", Name = nameof(GetValue))]
    public async Task<IActionResult> GetValue([FromRoute] int platformId, [FromRoute] int commandId, CancellationToken cancellationToken)
    {
        var result = await commandService.GetById(platformId, commandId, cancellationToken);

        return result.IsError
            ? NotFound()
            : Ok(result.Value.Adapt<CommandDto>());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCommandRequest createCommandRequest, [FromRoute] int platformId, CancellationToken cancellationToken)
    {
        if ((await platformService.GetByIdAsync(platformId, cancellationToken)) == Error.NotFound())
        {
            return NotFound("Platform not found");
        }

        var command = createCommandRequest.Adapt<Command>();
        command.PlatformId = platformId;

        var result = await commandService.CreateAsync(command, cancellationToken);

        return result.IsError
            ? BadRequest(result.Errors)
            : CreatedAtRoute(nameof(GetValue), new { platformId = result.Value.PlatformId, commandId = result.Value.Id }, result.Value.Adapt<CommandDto>());
    }
}