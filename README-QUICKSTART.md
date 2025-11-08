# Customer Management System - Quick Start Guide

A clean architecture CRUD application for managing customer data, built with .NET 7, Blazor WebAssembly, and SQL Server.

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for database)

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd crud-test-dotnet
```

### 2. Start the Database

Using PowerShell:

```powershell
.\start-database.ps1
```

Or using Docker Compose directly:

```bash
docker-compose up -d
```

Wait for the database to be ready (about 10-15 seconds).

### 3. Apply Database Migrations

```bash
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

This will create the `CustomerDb` database and all required tables.

### 4. Run the Application

```bash
dotnet run --project Mc2.CrudTest.Presentation/Server
```

The application will start on:
- **HTTPS**: `https://localhost:7266`
- **HTTP**: `http://localhost:5266`
- **API**: `https://localhost:7266/api/customers`
- **Swagger**: `https://localhost:7266/swagger`

### 5. Open in Browser

Navigate to `https://localhost:7266` and start managing customers!

## Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Unit Tests Only

```bash
dotnet test Mc2.CrudTest.UnitTests
```

### Run BDD/Acceptance Tests Only

```bash
dotnet test Mc2.CrudTest.AcceptanceTests
```

## Project Structure

```
📁 Mc2.CrudTest/
├── 📁 Mc2.CrudTest.Domain/              # Domain entities and value objects
├── 📁 Mc2.CrudTest.Application/         # CQRS commands, queries, handlers
├── 📁 Mc2.CrudTest.Infrastructure/      # EF Core, database, repositories
├── 📁 Mc2.CrudTest.Contracts/           # Shared DTOs
├── 📁 Mc2.CrudTest.Presentation/
│   ├── 📁 Server/                       # ASP.NET Core Web API
│   ├── 📁 Client/                       # Blazor WebAssembly UI
│   └── 📁 Shared/                       # Shared resources
├── 📁 Mc2.CrudTest.UnitTests/           # Unit tests (xUnit)
└── 📁 Mc2.CrudTest.AcceptanceTests/     # BDD tests (SpecFlow)
```

## Architecture

This application follows **Clean Architecture** principles:

- **Domain Layer**: Business entities, value objects, and rules
- **Application Layer**: Use cases (CQRS with MediatR)
- **Infrastructure Layer**: Data access (EF Core, SQL Server)
- **Presentation Layer**: Web API + Blazor WebAssembly UI
- **Contracts Layer**: Shared DTOs

## Features

✅ **CRUD Operations** for customers  
✅ **Domain-Driven Design** (DDD) with entities and value objects  
✅ **CQRS Pattern** using MediatR  
✅ **Test-Driven Development** (TDD) with unit tests  
✅ **Behavior-Driven Development** (BDD) with SpecFlow  
✅ **Email validation** with format checking  
✅ **Phone validation** using Google libphonenumber (mobile only, E.164 format)  
✅ **IBAN validation** for bank account numbers  
✅ **Unique email constraint** (per FirstName + LastName combination)  
✅ **RESTful API** with Swagger documentation  
✅ **Blazor WebAssembly** frontend  

## Database Management

### View Database Status

```bash
docker-compose ps
```

### View Database Logs

```bash
docker-compose logs -f sqlserver
```

### Stop Database (keeps data)

```bash
docker-compose down
```

Or using PowerShell:

```powershell
.\stop-database.ps1
```

### Reset Database (removes all data)

```bash
docker-compose down -v
docker-compose up -d
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

### Create New Migration

```bash
dotnet ef migrations add <MigrationName> --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

## Connection String

```
Server=localhost,1433;Database=CustomerDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true
```

Configured in `Mc2.CrudTest.Presentation/Server/appsettings.json`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/customers` | Get all customers |
| GET | `/api/customers/{id}` | Get customer by ID |
| GET | `/api/customers/by-email/{email}` | Get customer by email |
| POST | `/api/customers` | Create new customer |
| PUT | `/api/customers/{id}` | Update customer |
| DELETE | `/api/customers/{id}` | Delete customer |

## Customer Validation Rules

- **FirstName**: Required, 1-50 characters
- **LastName**: Required, 1-50 characters
- **DateOfBirth**: Required, must be in the past
- **PhoneNumber**: Required, must be valid mobile phone in E.164 format (e.g., +14155552671)
- **Email**: Required, must be valid email format, unique per FirstName+LastName
- **BankAccountNumber**: Required, must be valid IBAN format

## Troubleshooting

### Port Already in Use

If ports 5266/7266 are in use, update in `Mc2.CrudTest.Presentation/Server/Properties/launchSettings.json`

### Database Connection Failed

1. Check Docker is running: `docker ps`
2. Check SQL Server container: `docker-compose logs sqlserver`
3. Verify port 1433 is not in use: `netstat -an | findstr 1433`

### Migration Errors

```bash
# Remove last migration
dotnet ef migrations remove --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server

# List migrations
dotnet ef migrations list --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

## Development

### Build Solution

```bash
dotnet build
```

### Clean Build

```bash
dotnet clean
dotnet build --no-incremental
```

### Watch Mode (auto-reload)

```bash
dotnet watch --project Mc2.CrudTest.Presentation/Server
```

## License

This is a test project for evaluating software engineering practices.

