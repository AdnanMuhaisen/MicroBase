using Grpc.Core;
using MicroBase.PlatformService.Application.Interfaces;

namespace MicroBase.PlatformService.Api.Grpc.Services;

public class GrpcPlatformService(IPlatformService platformService) : GrpcPlatform.GrpcPlatformBase
{
    public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var platformResponse = new PlatformResponse();
        var platforms = await platformService.GetAllAsync(CancellationToken.None);
        foreach (var platform in platforms)
        {
            platformResponse.Platforms.Add(new GrpcPlatformModel
            {
                Name = platform.Name,
                PlatformId = platform.Id,
                Publisher = platform.Publisher
            });
        }

        return platformResponse;
    }
}