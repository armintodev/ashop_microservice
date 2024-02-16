FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER app
WORKDIR /app

#EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AShop.CatalogService/99-CatalogApi/99-CatalogApi.csproj", "src/AShop.CatalogService/99-CatalogApi/"]
COPY ["src/AShop.CatalogService/02-Persistence/02-Persistence.csproj", "src/AShop.CatalogService/02-Persistence/"]
COPY ["src/AShop.CatalogService/01-Domain/01-Domain.csproj", "src/AShop.CatalogService/01-Domain/"]
RUN dotnet restore "./src/AShop.CatalogService/99-CatalogApi/./99-CatalogApi.csproj"
COPY . .
WORKDIR "/src/src/AShop.CatalogService/99-CatalogApi"
RUN dotnet build "./99-CatalogApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./99-CatalogApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "99-CatalogApi.dll"]

#ENTRYPOINT ["dotnet", "99-CatalogApi.dll","--urls=https://+:443"] # when you want to use specific URLS on running app