﻿using FluentValidation;
using FluentValidation.AspNetCore;
using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Infrastructure.Data;
using MicroBase.PlatformService.Infrastructure.Options;
using MicroBase.PlatformService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.PlatformService.Api.Extensions;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(DependencyInjectionExtensions).Assembly);

        services.AddGrpc();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddDbContext<AppDbContext>(options =>
        {
            options
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services
            .AddOptions<RabbitMQSettings>()
            .Bind(configuration.GetSection("RabbitMQSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IMessageBusClient, MessageBusClient>();
        // services.AddScoped<ICommandServiceClient, HttpCommandServiceClient>();
        services.AddScoped<IPlatformService, Infrastructure.Services.PlatformService>();

        return services;
    }
}