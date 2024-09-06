using FluentValidation;
using FluentValidation.AspNetCore;
using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroBase.PlatformService.Api;

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

        services.AddScoped<IPlatformService, Infrastructure.Services.PlatformService>();

        return services;
    }
}