using Grpc.Net.Client;
using MicroBase.CommandService.Application.Interfaces;
using MicroBase.CommandService.Domain.Entities;
using MicroBase.PlatformService.Api;

namespace MicroBase.CommandService.Api.Grpc.Clients;

public class GrpcPlatformDataClient(IConfiguration configuration, ILogger<GrpcPlatformDataClient> logger) : IGrpcPlatformDataClient
{
    public IEnumerable<Platform> GetAllPlatforms()
    {
        logger.LogInformation($"Calling the grpc service at {DateTime.Now}");
        var grpcChannel = GrpcChannel.ForAddress(configuration["GrpcEndpoints:Platform"]!);
        var grpcClient = new GrpcPlatform.GrpcPlatformClient(grpcChannel);
        var requset = new GetAllRequest();

        try
        {
            List<Platform> result = [];
            var response = grpcClient.GetAllPlatforms(requset);
            foreach (var platform in response.Platforms)
            {
                result.Add(new()
                {
                    Name = platform.Name,
                    ExternalId = platform.PlatformId
                });
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);

            return null!;
        }
    }
}