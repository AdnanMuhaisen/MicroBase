﻿namespace MicroBase.CommandService.Api.Dtos;

public record PlatformDto
{
    public int Id { get; set; }

    public int ExternalId { get; set; }

    public string Name { get; set; } = null!;
}