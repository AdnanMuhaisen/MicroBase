FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroBase.PlatformService.Api/MicroBase.PlatformService.Api.csproj", "MicroBase.PlatformService.Api/"]
COPY ["MicroBase.PlatformService.Infrastructure/MicroBase.PlatformService.Infrastructure.csproj", "MicroBase.PlatformService.Infrastructure/"]
COPY ["MicroBase.PlatformService.Application/MicroBase.PlatformService.Application.csproj", "MicroBase.PlatformService.Application/"]
COPY ["MicroBase.PlatformService.Domain/MicroBase.PlatformService.Domain.csproj", "MicroBase.PlatformService.Domain/"]
RUN dotnet restore "./MicroBase.PlatformService.Api/MicroBase.PlatformService.Api.csproj"
COPY . .
WORKDIR "/src/MicroBase.PlatformService.Api"
RUN dotnet build "./MicroBase.PlatformService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroBase.PlatformService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroBase.PlatformService.Api.dll"]