#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER app
WORKDIR /app

EXPOSE 443
#EXPOSE 80 # comment this out if you want to

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AShop.DiscountService/Discount/Discount.csproj", "src/AShop.DiscountService/Discount/"]
RUN dotnet restore "./src/AShop.DiscountService/Discount/Discount.csproj"
COPY . .
WORKDIR "/src/src/AShop.DiscountService/Discount"
RUN dotnet build "./Discount.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Discount.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.dll"]