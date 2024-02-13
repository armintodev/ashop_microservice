#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER app
WORKDIR /app

EXPOSE 443
#EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AShop.BasketService/Basket/Basket.csproj", "src/AShop.BasketService/Basket/"]
RUN dotnet restore "./src/AShop.BasketService/Basket/./Basket.csproj"
COPY . .
WORKDIR "/src/src/AShop.BasketService/Basket"
RUN dotnet build "./Basket.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Basket.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.dll"]