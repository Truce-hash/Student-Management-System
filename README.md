# SOD316C Student Management System

An ASP.NET Core MVC student-management application developed for the SOD316C project. The application provides a database-backed interface for managing student records and demonstrates authentication and role-based authorization with ASP.NET Core Identity.

## Features

- Student record management
- SQLite database integration with Entity Framework Core
- ASP.NET Core Identity authentication
- Role-based authorization for `Admin` and `User` roles
- MVC views and Razor Pages
- Paginated student records
- Repository pattern and database initialization
- Centralized exception handling

## Technology

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core 8
- ASP.NET Core Identity
- SQLite
- Razor Views

## Project Structure

- `Controllers/` — MVC request handlers
- `Models/` — Student and application models
- `Views/` — Razor MVC views
- `Areas/Identity/` — ASP.NET Identity pages
- `Data/` — database context and initialization
- `Repositories/` — data-access implementations
- `Interfaces/` — repository abstractions
- `Migrations/` — Entity Framework migrations
- `Database/` — SQLite database files

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio or Visual Studio Code

### Run the Application

1. Open `ASPNETCore_DB.sln` in Visual Studio.
2. Restore the NuGet packages.
3. Build the solution.
4. Run the application.

The application uses SQLite as configured through `appsettings.json` and seeds an administrator account when it first starts.

## Default Administrator

- **Email:** `admin@gmail.com`
- **Password:** `Test!123`

Change the seeded credentials before using this project outside a local development environment.

## Project Scope

The project focuses on implementing and demonstrating authentication and authorization, including account login, roles, protected application areas, and role-based access control.
