# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy everything and restore
COPY . .
RUN dotnet restore "./SpamMusubiAPI/SpamMusubiAPI.csproj"

# build and publish
RUN dotnet publish "./SpamMusubiAPI/SpamMusubiAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SpamMusubiAPI.dll"]
