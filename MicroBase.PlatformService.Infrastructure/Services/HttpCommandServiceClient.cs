using MicroBase.PlatformService.Application.Interfaces;
using MicroBase.PlatformService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace MicroBase.PlatformService.Infrastructure.Services;

public class HttpCommandServiceClient : ICommandServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task SendPlatformAsync(Platform platform, CancellationToken cancellationToken)
    {
        string requestUrl = $"{_configuration["CommandServiceSettings:BaseUrl"]}/api/command-service/platforms";
        var stringContent = new StringContent(JsonSerializer.Serialize(platform), Encoding.UTF8, MediaTypeNames.Application.Json);
        using var response = await _httpClient.PostAsync(requestUrl, stringContent, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Successful Command Service Request");
        }
        else
        {
            Console.WriteLine("Unsuccessful Command Service Request");
        }
    }
}