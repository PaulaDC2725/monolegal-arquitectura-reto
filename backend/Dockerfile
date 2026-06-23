
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Monolegal.sln", "./"]
COPY ["Monolegal.Api/Monolegal.Api.csproj", "Monolegal.Api/"]
COPY ["Monolegal.Domain/Monolegal.Domain.csproj", "Monolegal.Domain/"]
COPY ["Monolegal.Infrastructure/Monolegal.Infrastructure.csproj", "Monolegal.Infrastructure/"]

RUN dotnet restore "Monolegal.sln"

COPY . .

WORKDIR "/src/Monolegal.Api"
RUN dotnet build "Monolegal.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Monolegal.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Monolegal.Api.dll"]