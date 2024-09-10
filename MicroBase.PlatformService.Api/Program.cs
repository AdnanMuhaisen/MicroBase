using MicroBase.PlatformService.Api.Extensions;
using MicroBase.PlatformService.Api.Grpc.Services;
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
app.MapGrpcService<GrpcPlatformService>();
app.MapGet("/protos/platforms", async (context)
    => await context.Response.WriteAsync(File.ReadAllText("Grpc/Protos/platforms.proto")));
await app.MigrateDatabaseAsync(app.Environment, CancellationToken.None);

app.Run();