# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "DartsApp.Server/DartsApp.Server/DartsApp.Server.csproj"
RUN dotnet publish "DartsApp.Server/DartsApp.Server/DartsApp.Server.csproj" -c Release -o /publish /p:UseAppHost=false

# deploy
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /publish .
ENTRYPOINT ["dotnet", "DartsApp.Server.dll"]