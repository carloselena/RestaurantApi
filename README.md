# Restaurant API

Restaurant API is a RESTful API built with ASP.NET Core, designed as a solid and scalable foundation for restaurant-related applications. The project focuses on clean architecture principles and software design best practices, emphasizing maintainability, separation of concerns, and long-term scalability. It also includes a complete user management and authentication system, covering registration, email confirmation, secure access, and credential recovery.

## Technologies

- ASP.NET Core Web API
- Entity Framework
- Generic Repository and Service
- AutoMapper
- Identity
- JWT
- SQL Server
- SMTP Client for email delivery
- Swagger / OpenAPI with documentation

Services will be replaced by the CQRS and Mediator patterns in order to
improve separation of concerns, reduce coupling between application layers,
and make business logic easier to maintain, test, and evolve over time.

## Architecture

The project follows the **ONION Architecture**, applying **SOLID principles** to achieve a clean, decoupled, and testable design.

### Layers:

- **Domain**

  - Contains business entities and core business rules.
  - Independent from any external framework.

- **Application**

  - Holds business logic, services, DTOs, validators, wrappers and contracts.
  - Defines interfaces implemented by the infrastructure layer.

- **Infrastructure**

  - Handles data access, Identity implementation, email delivery, and external integrations.
  - Depends on Application and Domain.

- **API**
  - Exposes REST endpoints through controllers.
  - Manages authentication, authorization, and request validation.

This structure promotes:

- Single Responsibility
- Dependency Inversion
- Open/Closed Principle
- High cohesion and low coupling

## Authentication & Users

The API implements authentication using **ASP.NET Identity and JWT**, providing:

- User registration
- Email confirmation
- Secure login
- Password recovery and reset
- Basic user and role management

Email-based workflows such as account verification and password recovery are handled via **SMTP**.

## Running the project

### Steps

1. Clone the repository
2. Configure DB connection, MailSettings and JWTSettings using appsettings.json or user-secrets
3. Apply DB migrations
4. Run the application
