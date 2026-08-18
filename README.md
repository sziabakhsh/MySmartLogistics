# 🚚 Multi-Tenant Logistics System (.NET 8 & Azure Cosmos DB)

A scalable, multi-tenant backend architecture built with **.NET 8 Web API** and **Azure Cosmos DB (NoSQL API)** using Clean Architecture principles and repository pattern over document storage.

---

## 🌟 Key Features

- **Multi-Tenancy**: Built-in tenant isolation using `ITenantProvider` and Cosmos DB Partition Keys (`TenantId`).
- **Clean Architecture**: Decoupled presentation, application core, domain layer, and infrastructure layers.
- **Azure Cosmos DB Integration**: High-throughput CRUD operations for `Orders`, `Warehouses`, and `Shipments`.
- **Domain Capabilities**:
  - **Orders Management**: Order items, status tracking, and address mapping.
  - **Warehouse & Inventory**: Capacity tracking and inventory SKU management.
  - **Shipment & Checkpoints**: Real-time checkpoint updates and tracking code creation.
- **RESTful Endpoints**: Swagger-documented API endpoints.

---

## 🏗️ Architecture Overview
src/
├── MyLogistics.Domain/          # Core Domain Entities, Enums, and Value Objects
├── MyLogistics.Application/     # DTOs, Interfaces (IShipmentService, IWarehouseService, etc.)
├── MyLogistics.Infrastructure/  # DbContext, Cosmos DB Configuration & Service Implementations
└── MyLogistics.Api/             # Web API Controllers & Dependency Injection setup

--

## 🛠️ Tech Stack & Dependencies

- **Framework**: .NET 8 Web API
- **Database**: Azure Cosmos DB (NoSQL Endpoint)
- **ORM / Driver**: Microsoft.EntityFrameworkCore.Cosmos
- **Documentation**: Swagger / OpenAPI
- **Testing**: xUnit / Moq *(optional)*

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Azure Cosmos DB Emulator (for local development) OR an active Azure Cosmos DB Account.

### Configuration

Update your `appsettings.json` or `appsettings.Development.json` in `MyLogistics.Api`:

```json
{
  "CosmosDb": {
    "AccountEndpoint": "https://localhost:8081",
    "AccountKey": "YOUR_COSMOS_DB_PRIMARY_KEY",
    "DatabaseName": "LogisticsDb"
  }
}

