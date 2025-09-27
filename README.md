
````markdown
# 🗺️ Map Service API (C# / ASP.NET Core)

A lightweight service for managing **Polygons** and **Objects** on an interactive map.  
Supports create, update, and delete operations with persistence in **MongoDB**.

---

## 🚀 Installation & Run

```bash
# 1. Clone repository
git clone https://github.com/ChaimCymerman0548492309/MapServer
cd MapServer

# 2. Restore dependencies
dotnet restore

# 3. Configure environment
cp appsettings.Development.json appsettings.Local.json
# edit "MongoSettings:ConnectionString"

# 4. Run locally
dotnet run --project MapServer

# 5. Run tests
dotnet test
````

Default server URL: **[http://localhost:4000](http://localhost:4000)**

---

## 📂 Data Schemas

### Polygon

```json
{
  "id": "string",
  "name": "string",
  "coordinates": [[[34.78,32.07],[34.79,32.07],[34.79,32.08],[34.78,32.08],[34.78,32.07]]]
}
```

### Object

```json
{
  "id": "string",
  "type": "jeep",
  "coordinates": [34.78, 32.07]
}
```

---

## 🌐 API Routes

| Method | Endpoint       | Description        |
| ------ | -------------- | ------------------ |
| GET    | /polygons      | Get all polygons   |
| POST   | /polygons      | Create new polygon |
| DELETE | /polygons/{id} | Delete polygon     |
| GET    | /objects       | Get all objects    |
| POST   | /objects       | Create new object  |
| DELETE | /objects/{id}  | Delete object      |

---

## ✅ Example Requests

### Create Polygon

```http
POST /polygons
Content-Type: application/json

{
  "name": "Area A",
  "coordinates": [[[34.78,32.07],[34.79,32.07],[34.79,32.08],[34.78,32.08],[34.78,32.07]]]
}
```

### Create Object

```http
POST /objects
Content-Type: application/json

{
  "type": "jeep",
  "coordinates": [34.78, 32.07]
}
```

---

## 🧪 Tests

Run with:

```bash
dotnet test
```

Covers:

* Polygon create / get / delete
* Object create / get / delete
* Validation & error handling

---

## 🔗 Flow Overview

```mermaid
flowchart LR
  Client[Map Client] -->|REST API| Server[ASP.NET Core]
  Server -->|CRUD| MongoDB[(MongoDB)]
```

---

```

---

```
