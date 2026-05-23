# Project Task Management API

A scalable RESTful API built with ASP.NET Core 9, following Clean Architecture principles.

## Architecture Overview

```
ProjectTaskManagement/
├── ProjectTaskManagement.API           
├── ProjectTaskManagement.Application   
├── ProjectTaskManagement.Domain        
└── ProjectTaskManagement.Infrastructure
```

- **API**: Controllers, Middleware, Program.cs
- **Application**: DTOs, Interfaces, Validators
- **Domain**: Entities, Enums, Exceptions
- **Infrastructure**: DbContext, Services, Migrations

## Tech Stack

- .NET 9 / ASP.NET Core Web API
- Entity Framework Core + SQL Server
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI

## Features

- JWT Authentication (Register & Login)
- Projects CRUD
- Tasks Management
- Role-based Authorization (Admin / User)
- Global Exception Handling
- Generic Response Wrapper
- Input Validation

## Setup Instructions

**1. Clone the repository**

```bash
git clone https://github.com/SandraEmad/ProjectTaskManagement.git
cd ProjectTaskManagement
```

**2. Update connection string in appsettings.Development.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ProjectManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

**3. Run migrations**

```bash
dotnet ef database update --project ProjectTaskManagement.Infrastructure --startup-project ProjectTaskManagement.API
```

**4. Run the project**

```bash
cd ProjectTaskManagement.API
dotnet run
```

**5. Open Swagger**

```
https://localhost:7068/swagger
```

## API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register new user |
| POST | /api/auth/login | Login |

### Projects
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/projects | Get all projects |
| GET | /api/projects/{id} | Get project by ID |
| POST | /api/projects | Create project |
| PUT | /api/projects/{id} | Update project |
| DELETE | /api/projects/{id} | Delete project |

### Tasks
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/tasks/project/{projectId} | Get tasks by project |
| POST | /api/tasks | Create task |
| PUT | /api/tasks/{id}/status | Update task status |
| DELETE | /api/tasks/{id} | Delete task |

## Authentication

All endpoints except `/api/auth/*` require JWT Bearer token.

```
Authorization: Bearer {your_token}
```
