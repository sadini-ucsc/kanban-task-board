# Kanban Backend (ASP.NET Core Web API)

Backend project for the Kanban Task Board.

## Tech Stack

- ASP.NET Core Web API (.NET 10)
- C#
- Entity Framework Core
- InMemory database 

## Project Structure

The solution follows Clean Architecture style with clear separation of concerns.

```
kanban-board.slnx
│
├── kanban.api
│ → Presentation layer (Controllers, API endpoints)
│
├── kanban.application
│ → Application layer (DTOs, business logic orchestration)
│
├── kanban.infrastructure
│ → Infrastructure layer (Database, EF Core, repositories)
│
├── kanban.domain
│ → Domain layer (Entities, enums)
```

## Setup

### Run project

Run the kanban.api project