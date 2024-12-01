# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-focal AS build
WORKDIR /source

COPY . .
RUN dotnet restore "./Shop.Server/Shop.Server.csproj" --disable-parallel
RUN dotnet publish "./Shop.Server/Shop.Server.csproj" -c release -o /app --no-restore

# Server Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Shop.Server.dll"]
