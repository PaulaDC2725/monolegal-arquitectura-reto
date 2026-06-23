FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS basic
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "Monolegal.sln"
RUN dotnet publish "Monolegal.sln" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monolegal.Api.dll"]