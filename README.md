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
docker build --rm -t productive-dev/Shop:latest .
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
