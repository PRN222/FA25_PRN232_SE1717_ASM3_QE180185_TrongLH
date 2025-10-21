FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EVCharging.ConsoleApp.TrongLH/EVCharging.ConsoleApp.TrongLH.csproj", "EVCharging.ConsoleApp.TrongLH/"]
RUN dotnet restore "EVCharging.ConsoleApp.TrongLH/EVCharging.ConsoleApp.TrongLH.csproj"
COPY . .
WORKDIR "/src/EVCharging.ConsoleApp.TrongLH"
RUN dotnet build "./EVCharging.ConsoleApp.TrongLH.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EVCharging.ConsoleApp.TrongLH.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EVCharging.ConsoleApp.TrongLH.dll"]
