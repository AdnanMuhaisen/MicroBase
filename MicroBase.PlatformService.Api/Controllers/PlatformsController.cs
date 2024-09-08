using ErrorOr;
using Mapster;
using MicroBase.PlatformService.Api.Commands;
using MicroBase.PlatformService.Api.Dtos;
using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroBase.PlatformService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController(IPlatformService platformService, ICommandServiceClient commandServiceClient) : ControllerBase
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
        
        try
        {
            await commandServiceClient.SendPlatformAsync(result.Value.Adapt<Platform>(), cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return result.IsError ? BadRequest(result.Errors) : CreatedAtRoute(nameof(GetValue), new { id = result.Value.Id }, result.Value);
    }
}