FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["Shop.Server/Shop.Server.csproj", "Shop.Server/"]
COPY ["shop.client/package.json", "shop.client/"]

RUN dotnet restore "Shop.Server/Shop.Server.csproj"

RUN apt-get update && apt-get install -y nodejs npm

COPY . .

WORKDIR /src/shop.client
RUN npm install
RUN npm run build

# RUN ls -alh /src/Shop.Server
WORKDIR /src/Shop.Server
RUN dotnet restore
RUN cat Shop.Server.csproj
RUN dotnet publish "./Shop.Server.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app .

# RUN ls -alh /src/shop.client
# COPY --from=build /src/shop.client/build ./shop.client/build

EXPOSE 5022  
# EXPOSE 3000

ENTRYPOINT ["dotnet", "Shop.Server.dll"]
