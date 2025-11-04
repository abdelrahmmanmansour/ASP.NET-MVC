# 🏢 Company Management System


🎥 [Watch Demo Video](https://drive.google.com/drive/folders/15d9nQMW3lHCYYhv8JGl3CcqFGJb3yRQC?usp=sharin)

**ASP.NET Core MVC | Entity Framework Core | SQL Server | Repository & Unit of Work | Dependency Injection | AutoMapper | Bootstrap | Razor Views**

## Table of Contents
- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Features](#features)
- [Technologies](#technologies)

## Project Overview
This is a **Company Management System** built with **ASP.NET Core MVC** following **N-Tier Architecture** to provide a **scalable, maintainable, and robust solution** for managing company operations. The system implements a clean separation of concerns, allowing easy maintainability and extensibility.

## Architecture
The project follows a layered architecture:

Presentation Layer → MVC Controllers & Views
Business Logic Layer → Services
Data Access Layer → Repository + EF Core

- **Presentation Layer:** Handles user interaction using Razor Views and MVC controllers.
- **Business Logic Layer (Services):** Encapsulates business rules, processing logic, and validations.
- **Data Access Layer:** Implements Repository & Unit of Work pattern for managing database operations through **Entity Framework Core**.

**Key Design Patterns:** Repository, Unit of Work, Dependency Injection, AutoMapper for object mapping.

## Features
- User-friendly and responsive UI with **Bootstrap**
- CRUD operations for company entities
- Role-based operations and security
- Scalable **N-Tier Architecture**
- Clean separation of concerns
- **AutoMapper** integration for mapping between DTOs and entities
- Input validation and error handling

## Technologies
- **Backend:** ASP.NET Core MVC, C#
- **Database:** SQL Server, Entity Framework Core
- **Frontend:** Bootstrap, Razor Views
- **Patterns & Tools:** Repository Pattern, Unit of Work, Dependency Injection, AutoMapper
