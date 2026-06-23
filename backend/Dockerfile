FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo el contenido de la carpeta backend
COPY . .

RUN dotnet restore "Monolegal.API/Monolegal.API.csproj"
RUN dotnet publish "Monolegal.API/Monolegal.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Monolegal.API.dll"]