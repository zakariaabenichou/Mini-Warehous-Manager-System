# 📦 Mini Warehouse Management System (WMS) MVP

A modern, highly-engineered portfolio project built with **ASP.NET Core 10.0 MVC** and **SQL Server**. This application is tailored to demonstrate enterprise-grade software development practices in the supply chain and logistics domain (inspired by Arvato).

---

##  Key Features

*   **KPI Logistics Dashboard:** Real-time statistics displaying total product catalog count, total physical items in stock, and out-of-stock items, complete with a "recently added" preview.
*   **Rich Domain Model:** Core business rules (such as preventing negative inventory) are safely encapsulated inside the `Product` domain model, protecting data consistency.
*   **Transactional Stock Movements:** All stock updates (inbound/outbound) are logged as historical movement records and wrapped in secure SQL transactions.
*   **Modern MVC Architecture:** Clean separation of concerns. The Controller remains thin, delegating all operations to a decoupled Service layer while utilizing custom **ViewModels** to keep database models decoupled from the user interface.
*   **DevOps & Container Ready:** Packaged using a multi-stage Dockerfile and optimized `docker-compose.yml` that boots both the application and a dedicated SQL Server container.
*   **Automated Testing:** Fully covered by automated **xUnit Integration & Unit Tests** leveraging an EF Core In-Memory database.

---

##  Technical Stack & Patterns

*   **Backend:** C# | .NET 8.0 | ASP.NET Core MVC
*   **Database:** MS SQL Server | Entity Framework Core (Migrations)
*   **Frontend:** HTML5 | Razor Views | Bootstrap 5 | jQuery (Client-side validation)
*   **Testing:** xUnit | EF Core In-Memory Database
*   **DevOps:** Docker | Docker Compose | GitHub Actions (CI/CD Pipeline)
*   **Architectural Patterns:** Rich Domain Model, Transactional Services, Dependency Injection, ViewModels, Repository-like Service Layer

---

##  Run the Project

### Option A: Local Run (Visual Studio)
1. Open the solution `Mini-Warehous-Manager-System.sln` in **Visual Studio 2022**.
2. Run `Add-Migration InitialCreate` and `Update-Database` in the Package Manager Console to generate the local SQL Server database.
3. Press **F5** to launch the web application.

### Option B: Docker Run (Production-like)
If you have Docker Desktop running, you can launch the complete ecosystem (application + database) with a single CLI command:
```bash
docker-compose up --build
