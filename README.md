# 🛒 E-Commerce API

> A robust, scalable E-Commerce backend built with **Clean Architecture** principles on ASP.NET Core — designed for maintainability, testability, and long-term growth.

---

## 📖 Project Overview

**E-Commerce API** is a backend Web API for an online shopping platform, built using ASP.NET Core 8 and Clean Architecture (Onion Architecture).
The API is designed to provide the backend services required for an e-commerce application, including product browsing, shopping cart management, order processing, user authentication, and authorization.
The project focuses on clean separation of concerns, maintainability, scalability, and testability.

## 🛠️ Tech Stack

| Category | Technologies / Frameworks |
| :--- | :--- |
| **Backend** | ASP.NET Core 8 Web API, C#, Entity Framework Core, LINQ, ASP.NET Core Identity, JWT |
| **Architecture** | Clean Architecture, Onion Architecture, Repository Pattern, Specification Pattern, Factory Pattern, Dependency Injection |
| **Database & Cache** | SQL Server, Entity Framework Core, Redis |
| **Payment Gateway** | Stripe (Payment Intents, Checkout Sessions, Webhooks) |
| **Libraries & Tools** | AutoMapper, Swagger / OpenAPI, Global Exception Handling Middleware, Logging Framework |

---


## 🏗️ Architecture

The solution is organized into **4 concentric layers**, following Clean Architecture:

```
┌─────────────────────────────────────────────┐
│  E-Commerce.API (Entry Point)                │
│  Controllers, Middlewares, Extensions,       │
│  Factories, Program.cs                       │
└───────────────────┬───────────────────────────┘
                     │
┌────────────────────▼──────────────────────────┐
│  Infrastructure                                │
│  ├─ Presentation   (Controllers, Attributes)   │
│  └─ Presistence    (Data, Identity, Repos)     │
└────────────────────┬──────────────────────────┘
                     │
┌────────────────────▼──────────────────────────┐
│  Core                                          │
│  ├─ Domain               (Entities, Contracts, │
│  │                        Exceptions)          │
│  ├─ Services              (business logic,     │
│  │                        MappingProfiles,     │
│  │                        Specifications)      │
│  └─ Services.Abstraction  (Service interfaces) │
└─────────────────────────────────────────────────┘
```

## 🚀 Features

### 🔐 JWT Authentication & Authorization
* User authentication using JWT
* Role-based authorization
* ASP.NET Core Identity integration

### 🛍️ Product Management
* Browse products & view product details
* Product filtering, sorting, and pagination
* Support for product types and brands

### 🛒 Shopping Cart
* Add, update, and remove products from cart
* Persistent cart management

### 📦 Order Management
* Order creation and status tracking
* Delivery methods management
* Order processing workflow

### 💳 Stripe Payment Integration
* Payment Intent implementation
* Stripe Checkout Sessions
* Webhook handling for payment events
* Integration with the order checkout workflow

### ⚡ Redis Caching
* Distributed caching using Redis
* Reduces unnecessary database queries
* Improves API performance and response times

### 🔎 Specification Pattern
* Reusable and flexible database queries
* Built-in support for filtering, sorting, pagination, and eager loading of related entities

### 🗂️ Repository Pattern
* Abstraction of data access logic
* Complete separation between business logic and persistence layer

### 🔄 AutoMapper
* Clean object-to-object mapping between domain entities and DTOs

### 🛡️ Global Exception Handling
* Centralized exception handling middleware
* Consistent API error responses across all endpoints

### 📝 Logging
* Application logging using a dedicated logging framework
* Helps monitor application behavior and supports error tracking

### 💉 Dependency Injection
* Native ASP.NET Core Dependency Injection for loose coupling

---

## 📁 Folder Structure

```
E-Commerce (Solution)
│
├── Core/
│   ├── Domain/
│   │   ├── Contracts/          # Interfaces for repositories/services
│   │   ├── Entities/           # Core business entities
│   │   ├── Exceptions/         # Custom domain exceptions
│   │   └── Global.cs
│   │
│   ├── Services/
│   │   ├── Implementations/    # Business logic implementations
│   │   ├── MappingProfiles/    # AutoMapper profiles
│   │   ├── Specifications/     # Query specifications (filtering/sorting)
│   │   └── AssemblyReference.cs
│   │
│   └── Services.Abstraction/
│       └── Contracts/          # Service interfaces
│
├── Infrastructure/
│   ├── Presentation/
│   │   ├── Attributes/         # Custom action/controller attributes
│   │   └── Controllers/        # Shared/base controllers
│   │
│   └── Presistence/
│       ├── Data/                # DbContext & configurations
│       ├── Global.cs
│       ├── Identity/            # ASP.NET Core Identity setup
│       ├── Repositories/        # Repository implementations
│       ├── AssemblyReference.cs
│       └── SpecificationEvluator.cs
│
├── E-Commerce.API/
│   ├── Controllers/             # API endpoints
│   ├── Extensions/              # Service registration/DI extensions
│   ├── Factories/               # Object factories
│   ├── Middlewares/
│   │   └── GlobalExceptionHandlingMiddleware.cs
│   ├── Properties/
│   ├── wwwroot/
│   ├── appsettings.json
│   ├── E-Commerce.API.http
│   ├── Program.cs
│   └── WeatherForecast.cs
│
├── Shared/                       # Cross-cutting shared code
│
└── E-Commerce.sln
```

---

# 🚀 How to Run the Project

## Prerequisites
Make sure you have the following installed:
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* SQL Server
* Redis
* Visual Studio 2022 / VS Code / Rider

---

## Steps

### 1. Clone the repository
```bash
git clone https://github.com/manarghareeb/FoodDeliveryAPI.git
cd FoodDeliveryAPI
```

### 2. Configure Application Settings
The project requires database connection strings, JWT configuration, and Stripe credentials.  
For security reasons, **do not add real credentials or secret keys to `appsettings.json` or commit them to GitHub**.

The following values should be configured locally using **.NET User Secrets** or environment variables:
* `ConnectionStrings:DefaultConnection`
* `ConnectionStrings:IdentityConnection`
* `ConnectionStrings:RedisConnection`
* `JwtOptions:SecretKey`
* `StripeSettings:SecretKey`
* `StripeSettings:EndPointSecret`

#### Example configuration structure (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_DEFAULT_CONNECTION_STRING",
    "IdentityConnection": "YOUR_IDENTITY_CONNECTION_STRING",
    "RedisConnection": "YOUR_REDIS_CONNECTION"
  },
  "JwtOptions": {
    "Issuer": "https://localhost:7090",
    "Audience": "AngularProject",
    "SecretKey": "YOUR_JWT_SECRET_KEY",
    "ExpirationInDays": 30
  },
  "StripeSettings": {
    "SecretKey": "YOUR_STRIPE_SECRET_KEY",
    "EndPointSecret": "YOUR_STRIPE_ENDPOINT_SECRET"
  }
}
```

> ⚠️ **Security Note:** Never commit real database passwords, JWT secret keys, Stripe secret keys, or Stripe webhook signing secrets to GitHub.

---

### 3. Configure .NET User Secrets
Initialize User Secrets for the API project:
```bash
dotnet user-secrets init --project E-Commerce.API
```

Then add your local configuration values:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_DEFAULT_CONNECTION_STRING" --project E-Commerce.API
dotnet user-secrets set "ConnectionStrings:IdentityConnection" "YOUR_IDENTITY_CONNECTION_STRING" --project E-Commerce.API
dotnet user-secrets set "ConnectionStrings:RedisConnection" "YOUR_REDIS_CONNECTION" --project E-Commerce.API
dotnet user-secrets set "JwtOptions:SecretKey" "YOUR_JWT_SECRET_KEY" --project E-Commerce.API
dotnet user-secrets set "StripeSettings:SecretKey" "YOUR_STRIPE_SECRET_KEY" --project E-Commerce.API
dotnet user-secrets set "StripeSettings:EndPointSecret" "YOUR_STRIPE_ENDPOINT_SECRET" --project E-Commerce.API
```

---

### 4. Restore Dependencies
```bash
dotnet restore
```

---

### 5. Apply Database Migrations
Using the **.NET CLI**:
```bash
dotnet ef database update --project Presistence --startup-project E-Commerce.API
```
Or using Visual Studio **Package Manager Console**:
```bash
Update-Database
```

---

### 6. Run the API
```bash
dotnet run --project E-Commerce.API
```

---

## 📖 Explore the API
Once the application is running, you can test the API using the included `E-Commerce.API.http` file or Swagger UI:
* **Swagger URL:** `https://localhost:7090/swagger`

*(Note: The actual URL may vary depending on your local `launchSettings.json` configuration.)*

---

## 🔗 Connect With Me

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/manar-ghareeb)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/manarghareeb)
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:manarghareeb1973@gmail.com)

---

<p align="center">Built with ❤️ using Clean Architecture & ASP.NET Core</p>
