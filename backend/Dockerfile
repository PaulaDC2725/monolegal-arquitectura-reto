FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS basic
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Monolegal.sln"
RUN dotnet publish "Monolegal.sln" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monolegal.Api.dll"]