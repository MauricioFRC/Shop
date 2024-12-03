# Shop

## Compilation
1. Clone this repository

```sh
git clone https://github.com/MauricioFRC/Shop.git && cd Shop
```

### Chanage the Connection String in `appsettings.Development.json`
```json
"ConnectionStrings": {
    "Shop": "Host=localhost; Database=shop; Username=your_user; Password=your_password"
}
```

2. Run Migrations
```sh
dotnet ef database update -p ./Infrastructure -s ./Shop.Server
```

3. Compile the program
```sh
cd ./Shop.Server && dotnet build -c Release
```

## Compilation with Docker
```sh
docker build -t shop-app .
```

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["Shop.Server/Shop.Server.csproj", "Shop.Server/"]
COPY ["shop.client/package.json", "shop.client/"]

RUN dotnet restore "Shop.Server/Shop.Server.csproj"

RUN apt-get update && apt-get install -y nodejs npm

COPY . .
...
```

```sh
docker run -p 5022:5022 shop-app
```

### Entity Relationship Diagram
![ERD](/img/ER.jpg)

---

- **Users:** for customers and administrators.
`POST` `/api/create-user`

```json
{
    "name": "string",
    "password": "string"
    "role": "string"
}
```

- **Orders:** for orders.
- **OrderDetails:** para productos en cada pedido.
`GET` `/orders/{id}/details`
- **Categories:** para organizar productos.
`GET` `/categories/{id}/products`

- **Payments:** para registrar transacciones.
`POST` `/payments`
```json
{
}
```

`GET` `/payments/{id}/details`
```json
{
}
```
---
