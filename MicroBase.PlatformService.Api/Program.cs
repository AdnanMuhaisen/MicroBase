using MicroBase.PlatformService.Api.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);
Console.WriteLine($"{Assembly.GetExecutingAssembly().FullName} - Running Env is {builder.Environment.EnvironmentName}");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.MapControllers();
await app.MigrateDatabaseAsync(app.Environment, CancellationToken.None);

app.Run();