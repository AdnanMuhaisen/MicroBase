using Mapster;
using MicroBase.CommandService.Api.Dtos;
using MicroBase.CommandService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroBase.CommandService.Api.Controllers;

[ApiController]
[Route("api/command-service/[controller]")]
public class PlatformsController(IPlatformService platformService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok((await platformService.GetAllAsync(cancellationToken)).Adapt<IEnumerable<PlatformDto>>());
    }
}