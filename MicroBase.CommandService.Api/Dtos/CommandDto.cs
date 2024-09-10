namespace MicroBase.CommandService.Api.Dtos;

public record CommandDto
{
    public int Id { get; set; }

    public string Activity { get; set; } = null!;

    public string CommandLine { get; set; } = null!;

    public int PlatformId { get; set; }
}