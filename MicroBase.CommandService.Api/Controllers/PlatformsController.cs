using Microsoft.AspNetCore.Mvc;

namespace MicroBase.CommandService.Api.Controllers;

[ApiController]
[Route("api/command-service/[controller]")]
public class PlatformsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        Console.WriteLine("Post from Command Service");

        return Ok();
    }
}