using FluentValidation;
using FluentValidation.AspNetCore;
using MicroBase.CommandService.Api.Grpc.Clients;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Infrastructure.Data;
using MicroBase.CommandService.Infrastructure.Options;
using MicroBase.CommandService.Infrastructure.Services;
using MicroBase.CommandService.Infrastructure.Services.BackgroundServices;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.CommandService.Api.Extensions;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(DependencyInjectionRegister).Assembly);
        services.AddScoped<IGrpcPlatformDataClient, GrpcPlatformDataClient>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
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

        services.AddHostedService<MessageBusSubscriber>();
        services.AddScoped<IPlatformService, Infrastructure.Services.PlatformService>();
        services.AddSingleton<IEventProcessingService, EventProcessingService>();
        services.AddScoped<ICommandService, Infrastructure.Services.CommandService>();

        return services;
    }
}