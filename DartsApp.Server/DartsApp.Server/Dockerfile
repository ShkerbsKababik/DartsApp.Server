# base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080

# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/DartsApp.Server/DartsApp.Server
RUN dotnet publish DartsApp.Server.csproj -c Release -o /publish /p:UseAppHost=false

# final
FROM base AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "DartsApp.Server.dll"]