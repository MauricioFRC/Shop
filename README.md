# Shop

## Compilation
1. Clone this repository

```sh
git clone https://github.com/MauricioFRC/Shop.git && cd Shop
```

#### Chanage the Connection String in `appsettings.Development.json`
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

# Products Endpoints

---


# Categories Endpoints

---

# Users Endpoints

`POST` `/api/create-user`
```json
{
  "userName": "John Doe",
  "email": "johndoe@gmail.com",
  "password": "john123456"
}
```

`Response`
```json
{
  "userId": 1,
  "userName": "John Doe",
  "email": "johndoe@gmail.com",
  "role": "Customer"
}
```

`GET` `/search-user/{id}`

`/search-user/1`
```json
{
  "userId": 1,
  "userName": "John Doe",
  "email": "johndoe@gmail.com",
  "role": "Customer"
}
```

`GET` `/list-users`
```json
[
  {
    "userId": 1,
    "userName": "John Doe",
    "email": "johndoe@gmail.com",
    "role": "Customer"
  },
  {
    "userId": 2,
    "userName": "Test",
    "email": "test@gmail.com",
    "role": "Customer"
  },
  {
    "userId": 3,
    "userName": "Test2",
    "email": "test2@gmail.com",
    "role": "Customer"
  }
]
```

`PUT` `/update-user/{id}`

`/update-user/1`
```json
{
  "userName": "John",
  "email": "johndoe@gmail.com",
  "password": "john123456789"
}
```

`Response`
```json
{
  "userId": 1,
  "userName": "John",
  "email": "johndoe@gmail.com",
  "role": "Customer"
}
```

`DELETE` `/delete-user/{id}`

`/delete-user/1`
```json
{
  "userId": 1,
  "userName": "John Doe",
  "email": "johndoe@gmail.com",
  "role": "Customer"
}
```
---

# Orders Endpoints

---

# Order Details Endpoints

---

# Payments Endpoints

---
