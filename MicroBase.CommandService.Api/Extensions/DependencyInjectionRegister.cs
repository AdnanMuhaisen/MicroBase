using FluentValidation;
using FluentValidation.AspNetCore;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Infrastructure.Data;
using MicroBase.CommandService.Infrastructure.Services;
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

        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<ICommandService, Infrastructure.Services.CommandService>();

        return services;
    }
}