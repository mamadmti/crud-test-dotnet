# Customer Management System - CRUD Application

A production-ready CRUD application demonstrating **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS**, and **Test-Driven Development (TDD/BDD)** principles. Built with .NET 7, Blazor WebAssembly, Entity Framework Core, and SQL Server.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Domain Model](#domain-model)
- [API Endpoints](#api-endpoints)
- [Validation Rules](#validation-rules)
- [Testing](#testing)
- [Database Management](#database-management)
- [Development](#development)
- [Design Decisions](#design-decisions)

## 🎯 Overview

This project implements a customer management system with full CRUD (Create, Read, Update, Delete) operations. It serves as a demonstration of enterprise-level software engineering practices and clean code principles.

### Purpose

This application was built to showcase:

- **Clean Architecture**: Clear separation of concerns with independent, testable layers
- **Domain-Driven Design**: Rich domain models with encapsulated business logic
- **CQRS Pattern**: Separation of read and write operations using MediatR
- **Test-Driven Development**: Comprehensive unit tests and BDD acceptance tests
- **Modern UI**: Responsive Blazor WebAssembly single-page application
- **Best Practices**: FluentValidation, repository pattern, dependency injection, and more

### Customer Model

```
Customer {
    FirstName           // 1-50 characters
    LastName            // 1-50 characters
    DateOfBirth         // Must be in the past
    PhoneNumber         // Valid mobile number (E.164 format)
    Email               // Valid email format
    BankAccountNumber   // Valid IBAN format
}
```

## 🏗️ Architecture

This application follows **Clean Architecture** principles with clear dependency rules:

```
┌─────────────────────────────────────────┐
│          Presentation Layer             │
│   (ASP.NET Core API + Blazor WASM)     │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│         Application Layer               │
│    (CQRS Handlers, Validators)         │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│          Domain Layer                   │
│   (Entities, Value Objects, Rules)     │
└─────────────────────────────────────────┘
                  ▲
┌─────────────────┴───────────────────────┐
│       Infrastructure Layer              │
│  (EF Core, Repositories, Database)     │
└─────────────────────────────────────────┘
```

### Layer Responsibilities

#### 📘 Domain Layer (`Mc2.CrudTest.Domain`)

- **Entities**: `Customer` - Core business entity with encapsulated logic
- **Value Objects**: `Email`, `PhoneNumber`, `BankAccountNumber` - Immutable, validated domain concepts
- **Interfaces**: `ICustomerRepository` - Contracts for data access
- **Business Rules**: All domain logic is enforced within entities and value objects

#### 📙 Application Layer (`Mc2.CrudTest.Application`)

- **Commands**: `CreateCustomerCommand`, `UpdateCustomerCommand`, `DeleteCustomerCommand`
- **Queries**: `GetAllCustomersQuery`, `GetCustomerByIdQuery`, `GetCustomerByEmailQuery`
- **Handlers**: MediatR handlers for each command/query
- **Validators**: FluentValidation rules for commands
- **Behaviors**: `ValidationBehavior` - Pipeline for automatic validation

#### 📕 Infrastructure Layer (`Mc2.CrudTest.Infrastructure`)

- **DbContext**: Entity Framework Core configuration
- **Repositories**: Implementation of `ICustomerRepository`
- **Configurations**: Entity mappings and value object conversions
- **Migrations**: Database schema management

#### 📗 Presentation Layer (`Mc2.CrudTest.Presentation`)

- **Server**: ASP.NET Core Web API with RESTful endpoints
- **Client**: Blazor WebAssembly SPA with responsive UI
- **Controllers**: `CustomersController` - API endpoints
- **Services**: `CustomerService` - Client-side HTTP service

#### 📓 Contracts Layer (`Mc2.CrudTest.Contracts`)

- **DTOs**: `CustomerDto` - Data transfer objects shared between client and server
- **Purpose**: Prevents coupling between presentation layers

## ✨ Features

### Core Functionality

✅ **Create** customers with full validation  
✅ **Read** customers (list all, get by ID, get by email)  
✅ **Update** customer information  
✅ **Delete** customers  
✅ **Uniqueness Validation**: By FirstName + LastName + DateOfBirth  
✅ **Email Uniqueness**: Per customer identity

### Technical Features

✅ **Domain-Driven Design** with rich domain models  
✅ **CQRS Pattern** using MediatR  
✅ **Repository Pattern** for data access abstraction  
✅ **Value Objects** for email, phone, and bank account validation  
✅ **FluentValidation** with automatic pipeline execution  
✅ **Unit Tests** (xUnit) for all layers  
✅ **BDD Tests** (SpecFlow) for acceptance criteria  
✅ **RESTful API** with proper HTTP semantics  
✅ **Blazor WebAssembly** modern SPA frontend  
✅ **Docker Compose** for database containerization  
✅ **Entity Framework Core** with code-first migrations

### Validation Features

✅ **Email Validation**: Format checking with lowercase normalization  
✅ **Phone Validation**: Google libphonenumber (mobile only, E.164 format)  
✅ **IBAN Validation**: Bank account number format checking  
✅ **Optimized Storage**: Phone numbers stored as `varchar(20)` for minimal space

## 🛠️ Technologies

### Backend

- **.NET 7.0** - Core framework
- **ASP.NET Core 7.0** - Web API
- **Entity Framework Core 7.0** - ORM
- **SQL Server 2022** - Database
- **MediatR 13.1.0** - CQRS implementation
- **FluentValidation 11.11.0** - Validation rules
- **libphonenumber-csharp 8.13.47** - Phone number validation

### Frontend

- **Blazor WebAssembly** - SPA framework
- **Bootstrap 5** - UI styling

### Testing

- **xUnit 2.4.2** - Unit testing framework
- **SpecFlow 3.9** - BDD framework (Gherkin syntax)
- **Moq 4.18.4** - Mocking framework
- **FluentAssertions 6.12.1** - Assertion library
- **Microsoft.AspNetCore.Mvc.Testing** - Integration testing

### DevOps

- **Docker Compose** - Database containerization
- **EF Core Tools** - Migration management

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- PowerShell (for helper scripts)

### Quick Start

#### 1. Clone the Repository

```bash
git clone <repository-url>
cd crud-test-dotnet
```

#### 2. Start the Database

Using PowerShell:

```powershell
.\start-database.ps1
```

Or using Docker Compose directly:

```bash
docker-compose up -d
```

Wait 10-15 seconds for SQL Server to be ready.

#### 3. Apply Database Migrations

```bash
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

This creates the `CustomerDb` database with all required tables.

#### 4. Run the Application

```bash
dotnet run --project Mc2.CrudTest.Presentation/Server
```

The application starts on:

- **HTTPS**: `https://localhost:7266`
- **HTTP**: `http://localhost:5266`

#### 5. Open in Browser

Navigate to `https://localhost:7266` - you'll be automatically redirected to the customer management page.

## 📁 Project Structure

```
📁 crud-test-dotnet/
├── 📁 Mc2.CrudTest.Domain/                 # Domain Layer (Core Business Logic)
│   ├── 📁 Common/
│   │   └── ValueObject.cs                  # Base class for value objects
│   ├── 📁 Entities/
│   │   └── Customer.cs                     # Customer entity with business rules
│   ├── 📁 ValueObjects/
│   │   ├── Email.cs                        # Email validation & normalization
│   │   ├── PhoneNumber.cs                  # Phone validation (E.164, mobile only)
│   │   └── BankAccountNumber.cs            # IBAN validation
│   └── 📁 Interfaces/
│       └── ICustomerRepository.cs          # Repository contract
│
├── 📁 Mc2.CrudTest.Application/            # Application Layer (Use Cases)
│   ├── 📁 Commands/
│   │   ├── CreateCustomerCommand.cs        # Create command
│   │   ├── CreateCustomerCommandHandler.cs # Create handler
│   │   ├── UpdateCustomerCommand.cs        # Update command
│   │   ├── UpdateCustomerCommandHandler.cs # Update handler
│   │   ├── DeleteCustomerCommand.cs        # Delete command
│   │   └── DeleteCustomerCommandHandler.cs # Delete handler
│   ├── 📁 Queries/
│   │   ├── GetAllCustomersQuery.cs         # List all query
│   │   ├── GetAllCustomersQueryHandler.cs  # List all handler
│   │   ├── GetCustomerByIdQuery.cs         # Get by ID query
│   │   ├── GetCustomerByIdQueryHandler.cs  # Get by ID handler
│   │   ├── GetCustomerByEmailQuery.cs      # Get by email query
│   │   └── GetCustomerByEmailQueryHandler.cs # Get by email handler
│   ├── 📁 Validators/
│   │   ├── CreateCustomerCommandValidator.cs # Create validation rules
│   │   └── UpdateCustomerCommandValidator.cs # Update validation rules
│   └── 📁 Behaviors/
│       └── ValidationBehavior.cs           # MediatR pipeline behavior
│
├── 📁 Mc2.CrudTest.Infrastructure/         # Infrastructure Layer (Data Access)
│   ├── 📁 Data/
│   │   └── CustomerDbContext.cs            # EF Core DbContext
│   ├── 📁 Configurations/
│   │   └── CustomerConfiguration.cs        # Entity mapping & conversions
│   ├── 📁 Repositories/
│   │   └── CustomerRepository.cs           # Repository implementation
│   └── 📁 Migrations/
│       └── 20251108115921_InitialCreate.cs # Database schema
│
├── 📁 Mc2.CrudTest.Contracts/              # Shared Contracts
│   └── CustomerDto.cs                      # Data transfer object
│
├── 📁 Mc2.CrudTest.Presentation/           # Presentation Layer
│   ├── 📁 Server/                          # ASP.NET Core Web API
│   │   ├── Controllers/
│   │   │   └── CustomersController.cs      # RESTful API endpoints
│   │   ├── Program.cs                      # DI configuration & startup
│   │   └── appsettings.json                # Configuration
│   └── 📁 Client/                          # Blazor WebAssembly SPA
│       ├── 📁 Pages/
│       │   ├── Index.razor                 # Home (redirects to customers)
│       │   ├── Customers.razor             # Customer list page
│       │   └── CustomerForm.razor          # Create/Edit form
│       ├── 📁 Services/
│       │   └── CustomerService.cs          # HTTP client service
│       └── 📁 Shared/
│           ├── NavMenu.razor               # Navigation menu
│           └── MainLayout.razor            # App layout
│
├── 📁 Mc2.CrudTest.UnitTests/              # Unit Tests (xUnit)
│   ├── 📁 Domain/
│   │   ├── 📁 Entities/
│   │   │   └── CustomerTests.cs            # Customer entity tests
│   │   └── 📁 ValueObjects/
│   │       ├── EmailTests.cs               # Email validation tests
│   │       ├── PhoneNumberTests.cs         # Phone validation tests
│   │       └── BankAccountNumberTests.cs   # IBAN validation tests
│   └── 📁 Application/
│       ├── 📁 Commands/
│       │   ├── CreateCustomerCommandHandlerTests.cs
│       │   ├── UpdateCustomerCommandHandlerTests.cs
│       │   └── DeleteCustomerCommandHandlerTests.cs
│       ├── 📁 Queries/
│       │   ├── GetAllCustomersQueryHandlerTests.cs
│       │   └── GetCustomerByIdQueryHandlerTests.cs
│       └── 📁 Validators/
│           ├── CreateCustomerCommandValidatorTests.cs
│           └── UpdateCustomerCommandValidatorTests.cs
│
├── 📁 Mc2.CrudTest.AcceptanceTests/        # BDD Tests (SpecFlow)
│   ├── 📁 Features/
│   │   └── CustomerManager.feature         # Gherkin scenarios
│   ├── 📁 StepDefinitions/
│   │   └── CustomerStepDefinitions.cs      # Step implementations
│   ├── 📁 Drivers/
│   │   └── Driver.cs                       # Test infrastructure
│   └── 📁 Support/
│       └── TestWebApplicationFactory.cs    # Test server setup
│
├── docker-compose.yml                      # Docker Compose configuration
├── start-database.ps1                      # PowerShell script to start DB
├── stop-database.ps1                       # PowerShell script to stop DB
├── DOCKER.md                               # Docker documentation
└── readme.md                               # This file
```

## 📊 Domain Model

### Customer Entity

The `Customer` entity is the core domain model with encapsulated business logic:

```csharp
public class Customer
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public BankAccountNumber BankAccountNumber { get; private set; }

    // Business logic encapsulated in constructor
    public Customer(string firstName, string lastName, DateTime dateOfBirth,
                   string phoneNumber, string email, string bankAccountNumber)
    {
        // Validation and initialization
    }

    // Update method with business rules
    public void Update(string firstName, string lastName, DateTime dateOfBirth,
                      string phoneNumber, string email, string bankAccountNumber)
    {
        // Validation and updates
    }
}
```

### Value Objects

#### Email

- Validates email format using regex
- Normalizes to lowercase for consistency
- Immutable with equality based on value

#### PhoneNumber

- Uses Google libphonenumber for validation
- Ensures only mobile numbers are accepted
- Stores in E.164 format (e.g., `+14155552671`)
- Optimized storage: `varchar(20)` in database

#### BankAccountNumber

- Validates IBAN format
- Immutable value object
- Equality based on IBAN value

## 🌐 API Endpoints

### Base URL: `/api/customers`

| Method   | Endpoint                          | Description           | Request Body  | Response                                              |
| -------- | --------------------------------- | --------------------- | ------------- | ----------------------------------------------------- |
| `GET`    | `/api/customers`                  | Get all customers     | None          | `200 OK` with `CustomerDto[]`                         |
| `GET`    | `/api/customers/{id}`             | Get customer by ID    | None          | `200 OK` with `CustomerDto` or `404 Not Found`        |
| `GET`    | `/api/customers/by-email/{email}` | Get customer by email | None          | `200 OK` with `CustomerDto` or `404 Not Found`        |
| `POST`   | `/api/customers`                  | Create new customer   | `CustomerDto` | `201 Created` with `CustomerDto` or `400 Bad Request` |
| `PUT`    | `/api/customers/{id}`             | Update customer       | `CustomerDto` | `200 OK` with `CustomerDto` or `400/404`              |
| `DELETE` | `/api/customers/{id}`             | Delete customer       | None          | `204 No Content` or `404 Not Found`                   |

### Example Request/Response

#### Create Customer (POST)

```json
POST /api/customers
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1990-01-15",
  "phoneNumber": "+14155552671",
  "email": "john.doe@example.com",
  "bankAccountNumber": "DE89370400440532013000"
}
```

#### Response (201 Created)

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1990-01-15",
  "phoneNumber": "+14155552671",
  "email": "john.doe@example.com",
  "bankAccountNumber": "DE89370400440532013000"
}
```

#### Error Response (400 Bad Request)

```json
{
  "error": "A customer with the same FirstName, LastName, and DateOfBirth already exists."
}
```

## ✅ Validation Rules

### Customer Validation

| Field                 | Rules                                                               |
| --------------------- | ------------------------------------------------------------------- |
| **FirstName**         | Required, 1-50 characters, letters/spaces/hyphens only              |
| **LastName**          | Required, 1-50 characters, letters/spaces/hyphens only              |
| **DateOfBirth**       | Required, must be in the past, age must be reasonable (1-150 years) |
| **PhoneNumber**       | Required, must be valid mobile phone in E.164 format                |
| **Email**             | Required, valid email format, unique per FirstName+LastName         |
| **BankAccountNumber** | Required, valid IBAN format                                         |

### Business Rules

1. **Unique Customer**: No two customers can have the same combination of:

   - FirstName + LastName + DateOfBirth

2. **Unique Email**: Each email must be unique in the database

3. **Phone Number**:

   - Must be a valid mobile phone number
   - Validated using Google libphonenumber
   - Stored in E.164 format (e.g., `+14155552671`)
   - Database storage: `varchar(20)` for minimal space

4. **Email**:

   - Must be valid email format
   - Automatically normalized to lowercase

5. **Bank Account**:
   - Must be a valid IBAN

## 🧪 Testing

This project follows **Test-Driven Development (TDD)** and **Behavior-Driven Development (BDD)** methodologies.

### Test Coverage

- **Domain Layer**: 100% coverage of entities and value objects
- **Application Layer**: All commands, queries, and validators tested
- **BDD Scenarios**: End-to-end acceptance tests

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

### Unit Tests (xUnit)

#### Domain Tests

- `CustomerTests`: Entity creation, updates, validation
- `EmailTests`: Email validation and normalization
- `PhoneNumberTests`: Phone validation, E.164 format
- `BankAccountNumberTests`: IBAN validation

#### Application Tests

- `CreateCustomerCommandHandlerTests`: Create logic and uniqueness checks
- `UpdateCustomerCommandHandlerTests`: Update logic and validation
- `DeleteCustomerCommandHandlerTests`: Delete logic
- `GetAllCustomersQueryHandlerTests`: Query all customers
- `GetCustomerByIdQueryHandlerTests`: Query by ID
- `CreateCustomerCommandValidatorTests`: Validation rules
- `UpdateCustomerCommandValidatorTests`: Validation rules

### BDD Tests (SpecFlow)

Located in `Mc2.CrudTest.AcceptanceTests/Features/CustomerManager.feature`

**Scenarios:**

- ✅ Create a new customer with valid details
- ✅ Reject duplicate customer (same FirstName, LastName, DateOfBirth)
- ✅ Reject customer with invalid mobile phone number
- ✅ Reject customer with invalid email address
- ✅ Update existing customer
- ✅ Delete existing customer
- ✅ Retrieve customer by ID
- ✅ Retrieve all customers

**Example Scenario:**

```gherkin
Scenario: Create a new customer with valid details
    Given I have a valid customer with the following details
        | Field              | Value                      |
        | FirstName          | John                       |
        | LastName           | Doe                        |
        | DateOfBirth        | 1990-01-01                 |
        | PhoneNumber        | +14155552671               |
        | Email              | john.doe@example.com       |
        | BankAccountNumber  | DE89370400440532013000     |
    When I create the customer
    Then the customer should be created successfully
```

## 🗄️ Database Management

### Connection String

```
Server=localhost,1433;Database=CustomerDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true
```

Configured in `Mc2.CrudTest.Presentation/Server/appsettings.json`

### Docker Commands

#### Start Database

```bash
docker-compose up -d
```

Or using PowerShell:

```powershell
.\start-database.ps1
```

#### Stop Database (keeps data)

```bash
docker-compose down
```

Or using PowerShell:

```powershell
.\stop-database.ps1
```

#### View Database Status

```bash
docker-compose ps
```

#### View Database Logs

```bash
docker-compose logs -f sqlserver
```

#### Reset Database (removes all data)

```bash
docker-compose down -v
docker-compose up -d
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

### Entity Framework Migrations

#### Apply Migrations

```bash
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

#### Create New Migration

```bash
dotnet ef migrations add <MigrationName> --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

#### List Migrations

```bash
dotnet ef migrations list --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

#### Remove Last Migration

```bash
dotnet ef migrations remove --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

### Database Schema

The database contains a single `Customers` table:

```sql
CREATE TABLE Customers (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,      -- E.164 format, minimal storage
    Email NVARCHAR(255) NOT NULL,
    BankAccountNumber NVARCHAR(34) NOT NULL
);
```

## 💻 Development

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

### Run with Specific Environment

```bash
dotnet run --project Mc2.CrudTest.Presentation/Server --environment Production
```

### Code Quality

This project follows:

- **SOLID Principles**
- **Clean Code** practices
- **Domain-Driven Design** patterns
- **Repository Pattern**
- **Dependency Inversion**
- **Single Responsibility Principle**

## 🎯 Design Decisions

### Why Clean Architecture?

- **Independence**: Business logic is independent of frameworks, UI, and database
- **Testability**: Easy to test with minimal dependencies
- **Flexibility**: Easy to swap implementations (e.g., change database)
- **Maintainability**: Clear separation of concerns

### Why Value Objects?

- **Encapsulation**: Validation logic lives with the data
- **Immutability**: Prevents accidental changes
- **Equality**: Value-based equality semantics
- **Domain Clarity**: Models business concepts explicitly

### Why CQRS?

- **Separation**: Read and write operations have different concerns
- **Scalability**: Can scale reads and writes independently
- **Clarity**: Clear distinction between commands (mutations) and queries
- **Optimization**: Different models for reads vs writes

### Why MediatR?

- **Decoupling**: Controllers don't depend directly on handlers
- **Pipeline**: Easy to add cross-cutting concerns (validation, logging)
- **Testability**: Each handler is independently testable
- **Simplicity**: Thin controllers, business logic in handlers

### Why FluentValidation?

- **Expressiveness**: Readable validation rules
- **Separation**: Validation logic separate from business logic
- **Testability**: Easy to test validators independently
- **Reusability**: Share validation rules across layers

### Why No Custom Exceptions?

- **Simplicity**: Standard exceptions are sufficient
- **Clarity**: Clear error messages without additional complexity
- **Practicality**: ArgumentException and ValidationException cover most cases

### Phone Number Storage Optimization

**Storage Format**: `varchar(20)` for E.164 format

**Why E.164?**

- International standard (e.g., `+14155552671`)
- Maximum length: 15 digits + 1 for '+' = 16 characters
- `varchar(20)` provides buffer for safety

**Space Comparison:**

- `varchar(20)`: 16-20 bytes (optimal)
- `ulong`: 8 bytes (but loses leading zeros, can't store '+')
- `nvarchar(50)`: 100+ bytes (wasteful)

**Decision**: `varchar(20)` balances storage efficiency with practical flexibility.

## 🔍 Troubleshooting

### Port Already in Use

If ports 5266/7266 are in use, update in:
`Mc2.CrudTest.Presentation/Server/Properties/launchSettings.json`

### Database Connection Failed

1. Check Docker is running: `docker ps`
2. Check SQL Server logs: `docker-compose logs sqlserver`
3. Verify port 1433 availability: `netstat -an | findstr 1433`
4. Wait 10-15 seconds after `docker-compose up` for SQL Server to initialize

### Migration Errors

```bash
# Remove last migration
dotnet ef migrations remove --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server

# Check migration status
dotnet ef migrations list --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

### Build Errors

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build --no-incremental
```

### Test Failures

Ensure database is fresh for BDD tests:

```bash
docker-compose down -v
docker-compose up -d
# Wait 15 seconds
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
dotnet test Mc2.CrudTest.AcceptanceTests
```

## 📝 License

This is a code test project demonstrating software engineering best practices.

## 🙏 Acknowledgments

- **Clean Architecture** by Robert C. Martin
- **Domain-Driven Design** by Eric Evans
- **CQRS** by Greg Young
- **SpecFlow** for BDD testing
- **Google libphonenumber** for phone validation

---

**Built with ❤️ using Clean Architecture, DDD, CQRS, and TDD/BDD**
