# 🛒 Shop Management System (POS)

A robust Point of Sale (POS) system built with **ASP.NET Core 9.0**, following **Clean Architecture** principles and the **CQRS** pattern.

## 🚀 Key Technical Features
- **Clean Architecture**: Strict separation of concerns into Domain, Application, Infrastructure, and API layers.
- **CQRS Pattern**: Decoupled Command (Write) and Query (Read) logic using **MediatR**.
- **Repository & Unit of Work**: Abstracted data access layer for better testability and maintenance.
- **Security**: **JWT Bearer Authentication** with Role-based access control (Admin/User).
- **Soft Delete**: Integrated data integrity using EF Core Global Query Filters.
- **Reporting**: 
  - **Excel Export**: Inventory reports generated using **ClosedXML**.
  - **PDF Invoices**: Professional order invoices created with **QuestPDF**.
- **Validation**: Professional request validation using **FluentValidation**.
- **Mapping**: Clean transformation between Entities and DTOs via **AutoMapper**.
- **Testing**: Comprehensive unit tests using **xUnit**, **Moq**, and **FluentAssertions**.

---

## 🏗️ Project Architecture
The solution is organized into four distinct layers:
1. **Domain**: Core entities (Product, Category, Customer, Order, OrderItem) and domain logic.
2. **Application**: Business logic, CQRS Commands/Queries, Handlers, DTOs, and interfaces.
3. **Infrastructure**: Data persistence (EF Core, SQL Server), Migrations, and external services (Auth, Excel/PDF).
4. **API**: RESTful controllers, Middleware, and configuration.

---

## 🛠️ Tech Stack
- **Backend**: .NET 9.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Mediator**: MediatR
- **Mapping**: AutoMapper
- **Testing**: xUnit, Moq, FluentAssertions
- **Reporting**: ClosedXML, QuestPDF
- **Documentation**: Swagger/OpenAPI

---

## 🔧 Installation & Setup

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup Steps
1. **Clone the repository**:
   ```bash
   git clone https://github.com/tuhazem/ShopProject.git
   cd ShopApp
   ```

2. **Update Connection String**:
   Open `ShopProject.API/appsettings.json` and update the `DefaultConnection` to point to your SQL Server instance.

3. **Apply Migrations**:
   ```bash
   dotnet ef database update --project ShopProject.Infrastructure --startup-project ShopProject.API
   ```

4. **Run the Application**:
   ```bash
   dotnet run --project ShopProject.API
   ```

---

## 📖 API Documentation
Once the application is running, you can access the interactive Swagger documentation at:
`http://localhost:5000/swagger` (or your configured port).

### Core Modules
- **Auth**: User registration and login (JWT).
- **Products**: Full CRUD with pagination and search.
- **Categories**: Category management.
- **Customers**: Management with soft-delete and restore capabilities.
- **Orders**: Transaction processing and stock management.
- **Actions**: Exporting Inventory (Excel) and Order Invoices (PDF).
- **Admin**: Dashboard statistics and KPIs.

---

## 🧪 Running Tests
The project includes unit tests for core application logic:
```bash
dotnet test ShopApp.Application.Tests/ShopApp.Application.Tests.csproj
```

---

## 👨‍💻 Author
**Hazem Mohamed** - [GitHub](https://github.com/tuhazem)
