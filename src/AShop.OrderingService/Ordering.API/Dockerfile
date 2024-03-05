FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

#EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/AShop.OrderingService/Ordering.API/Ordering.API.csproj", "src/AShop.OrderingService/Ordering.API/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/AShop.OrderingService/Ordering.Application/Ordering.Application.csproj", "src/AShop.OrderingService/Ordering.Application/"]
COPY ["src/AShop.OrderingService/Ordering.Domain/Ordering.Domain.csproj", "src/AShop.OrderingService/Ordering.Domain/"]
COPY ["src/AShop.OrderingService/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "src/AShop.OrderingService/Ordering.Infrastructure/"]
RUN dotnet restore "./src/AShop.OrderingService/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/AShop.OrderingService/Ordering.API"
RUN dotnet build "./Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]