# Kanban Task Board

A Kanban task management application built with a React frontend and a .NET backend.

## Overview

This project allows users to:
- View tasks
- Create tasks
- Edit tasks
- Delete tasks (soft delete)
- Drag and drop tasks between status columns (To Do, In Progress, Done)

## Tech Stack

### Frontend
- React
- Context API + useReducer
- @dnd-kit for drag and drop
- CSS (modular styling)

### Backend
- .NET Web API
- C#
- Entity Framework Core
- REST APIs
- InMemory database

### Unit Tests
- xUnit
- Moq
- FluentAssertions

## Project Structure
- /kanban-frontend
- /kanban-board

## High-Level Architecture
```
                ┌──────────────────────┐
                │     Kanban UI        │
                │   (React Frontend)   │
                └─────────┬────────────┘
                          │ HTTP (REST API)
                          ▼
                ┌──────────────────────┐
                │     Kanban API       │
                │   (Presentation)     │
                └─────────┬────────────┘
                          │
                          ▼
        ┌──────────────────────────────────┐
        │      Application Layer           │
        │  - Services (Business Logic)     │
        │  - DTOs                          │
        │  - Validators (FluentValidation) │
        └─────────────────┬────────────────┘
                          │
                          ▼
        ┌──────────────────────────────────┐
        │        Domain Layer              │
        │  - Entities (KanbanTask)         │
        │  - Enums (Status)                │
        └────────────────┬─────────────────┘
                          │
                          ▼
        ┌──────────────────────────────────┐
        │     Infrastructure Layer         │
        │  - Repositories                  │
        │  - Data persistence              │
        └──────────────────────────────────┘
```

## Getting Started

### Frontend
See kanban-frontend project README file for setup instructions.

### Backend
See kanban-board project README file for setup instructions.

## Continuous Integration (CI)
A GitHub Actions pipeline is configured to automatically:
- Restore dependencies
- Build the solution
- Run all unit tests

### CI triggers
The pipeline runs on:
- Pushes to main
- Pushes to develop (if added later as a layer before merging to main branch)
- Pull requests targeting main
- Pull requests targeting develop

## Development Workflow
- Create a feature branch
```
git checkout -b feature/branch-name
```
- Commit changes
```
git add .
git commit -m "feat: description"
git push origin feature/branch-name
```
- Open a pull request
- Select `main` as base branch
- CI runs automatically on PR creation
- Merge
- PR must pass CI checks before merging

## Assumptions
- A newly created task always starts in the `Todo` state
- The system is designed as a simple Kanban workflow without user authentication or multi-user ownership
- Task status transitions are not strictly enforced (e.g., tasks can move directly from `Todo` to `Done`)

### Future Improvements
- Branch protection rules (require CI pass before merge, prevent direct commits to `main` branch)
- Frontend CI pipeline
- Static code analysis (SonarQube)
- Integration tests
