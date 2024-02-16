#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER app
WORKDIR /app

EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateways/OcelotApiGw/OcelotApiGw.csproj", "src/ApiGateways/OcelotApiGw/"]
RUN dotnet restore "./src/ApiGateways/OcelotApiGw/OcelotApiGw.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/OcelotApiGw"
RUN dotnet build "./OcelotApiGw.csproj" -c $BUILD_CONFIGURATION -o /app/build

#if this file would be Updated, so uncomment the below command.
#RUN chmod 777 ocelot.json 

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OcelotApiGw.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGw.dll"]