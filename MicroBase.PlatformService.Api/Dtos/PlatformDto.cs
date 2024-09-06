namespace MicroBase.PlatformService.Api.Dtos;

public record PlatformDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public decimal Cost { get; set; }
}