# 🛒 Shop Management System (POS)
> A robust Point of Sale system built with **ASP.NET Core 9.0**, following **Clean Architecture** principles and **CQRS** pattern.

## 🚀 Key Technical Features
* **Clean Architecture**: Separation of concerns into Domain, Application, Infrastructure, and API layers.
* **CQRS Pattern**: Using **MediatR** for decoupling commands (Write) and queries (Read).
* **Repository & Unit of Work**: Optimized data access layer.
* **Soft Delete**: Data integrity maintained using Global Query Filters.
* **Fluent Validation**: Professional request validation.
* **AutoMapper**: Clean mapping between Entities and DTOs.
* **RESTful APIs**: Professional endpoint design tested via Postman & Swagger.

---

## 🏗️ Project Architecture
The project is divided into 4 main layers:
1.  **Domain**: Contains Entities, Enums, and Core logic.
2.  **Application**: Contains Commands, Queries, Handlers, and DTOs.
3.  **Infrastructure**: Contains DbContext, Migrations, and Repository implementations.
4.  **API**: The entry point, containing Controllers and Middleware.

---

## 🛠️ Tech Stack
* **Backend**: .NET 9.0
* **Database**: SQL Server
* **ORM**: Entity Framework Core
* **Mediator**: MediatR
* **Mapping**: AutoMapper
* **Documentation**: Swagger UI

---

## 🔧 Installation & Setup
1. Clone the repository:
   ```bash
   git clone [https://github.com/YourUsername/ShopProject.git](https://github.com/YourUsername/ShopProject.git)
