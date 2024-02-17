FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER app
WORKDIR /app

EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateways/Shopping.Aggregator/Shopping.Aggregator.csproj", "src/ApiGateways/Shopping.Aggregator/"]
RUN dotnet restore "./src/ApiGateways/Shopping.Aggregator/Shopping.Aggregator.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Shopping.Aggregator"
RUN dotnet build "./Shopping.Aggregator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shopping.Aggregator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Aggregator.dll"]