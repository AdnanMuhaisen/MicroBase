using ErrorOr;
using Mapster;
using MicroBase.PlatformService.Api.Commands;
using MicroBase.PlatformService.Api.Dtos;
using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Domain.Entities;
using MicroBase.PlatformService.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MicroBase.PlatformService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController(IPlatformService platformService, ICommandServiceClient commandServiceClient, IMessageBusClient messageBusClient) : ControllerBase
{
    [HttpGet(Name = nameof(Get))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok((await platformService.GetAllAsync(cancellationToken)).Adapt<List<PlatformDto>>());
    }

    [HttpGet("{id:int}", Name = nameof(GetValue))]
    public async Task<IActionResult> GetValue([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await platformService.GetByIdAsync(id, cancellationToken);

        return result.IsError ? NotFound() : Ok(result.Value.Adapt<PlatformDto>());
    }

    [HttpPost(Name = nameof(Post))]
    public async Task<IActionResult> Post([FromBody] CreatePlatformCommand createPlatformCommand, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await platformService.CreateAsync(createPlatformCommand.Adapt<Platform>(), cancellationToken);
        var platformToPublish = result.Value.Adapt<PlatformToPublish>();
        platformToPublish.Event = PlatformEvent.Published;
        messageBusClient.PublishNewPlatform(platformToPublish);

        return result.IsError ? BadRequest(result.Errors) : CreatedAtRoute(nameof(GetValue), new { id = result.Value.Id }, result.Value);
    }
}