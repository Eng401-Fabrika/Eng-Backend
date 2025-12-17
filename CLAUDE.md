# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Run Commands

```bash
# Build
dotnet build

# Run API (Swagger available at /swagger in development)
dotnet run --project Eng-BackendAPI/Eng-BackendAPI.csproj

# Database migrations
dotnet ef database update --project Eng-Backend.DAL/Eng-Backend.DAL.csproj --startup-project Eng-BackendAPI/Eng-BackendAPI.csproj

# Add new migration
dotnet ef migrations add <MigrationName> --project Eng-Backend.DAL/Eng-Backend.DAL.csproj --startup-project Eng-BackendAPI/Eng-BackendAPI.csproj

# Docker PostgreSQL
docker-compose up -d           # Start database
docker-compose down            # Stop database
make db-reset                  # Clean rebuild with migrations
```

## Architecture

This is a .NET 8 Web API using a layered architecture with ASP.NET Core Identity and PostgreSQL.

### Project Structure

- **Eng-BackendAPI**: API layer - Controllers, middleware, DI configuration
- **Eng-Backend.BusinessLayer**: Business logic - Services/Managers, interfaces, custom exceptions, utilities (JWT, hashing)
- **Eng-Backend.DAL**: Data Access Layer - EF Core DbContext, repositories, entities, migrations
- **Eng-Backend.DtoLayer**: DTOs for API requests/responses

### Key Patterns

**Generic Repository/Service Pattern**: `GenericRepository<T>` and `GenericManager<T>` provide base CRUD operations. Entity-specific repositories extend these (e.g., `UserRepository`, `RoleRepository`).

**Global Exception Handling**: Custom exceptions in `BusinessLayer/Exceptions/` (`BadRequestException`, `NotFoundException`, `UnauthorizedException`, `ForbiddenException`, `InternalServerException`) are caught by `GlobalExceptionHandlerMiddleware` and converted to standardized `ApiResponse<T>` format.

**Authentication**: JWT Bearer tokens via ASP.NET Core Identity. Token configuration in `appsettings.json` under `AppSettings:Token`.

### Database

PostgreSQL on port 5434 (mapped from container's 5432). Connection string in `appsettings.json`. Seed data includes default roles (Admin, User, GenericRole) and permissions for RBAC.

### Response Format

All API responses use `ApiResponse<T>`:
```json
{
  "success": true/false,
  "statusCode": 200,
  "message": "...",
  "data": { },
  "errors": []
}
```

Throw custom exceptions from services rather than returning error responses manually - the middleware handles conversion.
