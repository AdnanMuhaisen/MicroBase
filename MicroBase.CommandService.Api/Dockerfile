FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroBase.CommandService.Api/MicroBase.CommandService.Api.csproj", "MicroBase.CommandService.Api/"]
COPY ["MicroBase.CommandService.Infrastructure/MicroBase.CommandService.Infrastructure.csproj", "MicroBase.CommandService.Infrastructure/"]
COPY ["MicroBase.CommandService.Application/MicroBase.CommandService.Application.csproj", "MicroBase.CommandService.Application/"]
COPY ["MicroBase.CommandService.Domain/MicroBase.CommandService.Domain.csproj", "MicroBase.CommandService.Domain/"]
RUN dotnet restore "./MicroBase.CommandService.Api/MicroBase.CommandService.Api.csproj"
COPY . .
WORKDIR "/src/MicroBase.CommandService.Api"
RUN dotnet build "./MicroBase.CommandService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroBase.CommandService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroBase.CommandService.Api.dll"]