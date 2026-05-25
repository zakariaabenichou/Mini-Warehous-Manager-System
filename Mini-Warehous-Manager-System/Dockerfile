FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# SDK-Image zum Kompilieren
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Mini_Warehous_Manager_System.csproj", "./"]
RUN dotnet restore "Mini-warehous-manager-system.csproj"
COPY . .
RUN dotnet build "Mini_Warehous_Manager_System.csproj" -c Release -o /app/build

# Code veröffentlichen
FROM build AS publish
RUN dotnet publish "Mini_Warehous_Manager_System.csproj" -c Release -o /app/publish

# Finales Image zusammensetzen
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mini_Warehous_Manager_System.dll"]