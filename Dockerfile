# Use .NET 8 SDK for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore deps
COPY ["SpamMusubiAPI.csproj", "./"]
RUN dotnet restore "./SpamMusubiAPI.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "SpamMusubiAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SpamMusubiAPI.dll"]
