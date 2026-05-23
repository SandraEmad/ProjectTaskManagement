# Project Task Management API

## Architecture Overview
- Domain Layer: Entities, Enums, Exceptions
- Application Layer: DTOs, Interfaces, Validators
- Infrastructure Layer: DbContext, Services
- API Layer: Controllers, Middleware

## Setup Instructions
1. Clone the repository
2. Update connection string in appsettings.Development.json
3. Run migrations: dotnet ef database update
4. Run the project: dotnet run

## Tech Stack
- .NET 9
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger
