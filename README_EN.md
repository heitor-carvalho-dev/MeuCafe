# ☕ MeuCafé

A REST API inspired by [BuyMeACoffee](https://buymeacoffee.com) — a platform where content creators can receive symbolic donations from their supporters, in the format of "buying a coffee".

> 🚧 Project under active development.

---

## 🧱 Architecture

The project was built following **Clean Architecture** principles, with clear separation of concerns across layers:

```
MeuCafé/
├── Domain/           # Entities, interfaces and business rules (core)
├── Application/      # Use cases, DTOs
├── Infrastructure/   # Repository implementations, EF Core, mappings
├── API/              # Controllers, middlewares and app configuration
└── UI/               # (on hold)
```

Dependencies always flow inward — outer layers know inner layers, never the other way around.

---

## 🛠️ Tech Stack

- **ASP.NET Core** — main API framework
- **Entity Framework Core** — ORM for object-relational mapping
- **PostgreSQL** — relational database
- **Custom Middleware** — centralized error handling

---

## 🚀 Features

| Use Case | Description |
|---|---|
| Create user | Register a new content creator on the platform |
| Delete user | Remove an account and its associated data |
| List users | Retrieve all registered profiles |
| Warmup | Health-check route to verify API availability |

---

## ⚙️ Running locally

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Setup

1. Clone the repository:
```bash
git clone https://github.com/CupOfCakes/MeuCafe.git
cd MeuCafe
```

2. Set the connection string in `appsettings.json` (inside `API/`):
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=meucafe;Username=your_user;Password=your_password"
}
```

3. Apply migrations:
```bash
dotnet ef database update --project Infrastructure --startup-project API
```

4. Run the application:
```bash
dotnet run --project API
```

The API will be available at `https://localhost:5001`.

---

## 📌 Notes

- The `UI` layer is on hold — the project focus is the API.
- New features (donations, public profiles) are planned for upcoming versions.
