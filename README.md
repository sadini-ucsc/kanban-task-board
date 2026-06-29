# Kanban Task Board

A Kanban task management application built with a React frontend and a .NET backend.

## Overview

This project allows users to:
- View tasks
- Create tasks
- Edit tasks
- Delete tasks (soft delete)
- Drag and drop tasks between status columns (To Do, In Progress, Done)

## Assumptions
- A task must have a title (required field)
- The maximum allowed length for title is 100 characters
- The description is optional
- The maximum allowed length for description is 1000 characters
- A newly created task always starts in the `Todo` state
- The system is designed as a simple Kanban workflow without user authentication or multi-user ownership
- Task status transitions are not strictly enforced (e.g., tasks can move directly from `Todo` to `Done`)

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

### Future Improvements
- Branch protection rules (require CI pass before merge, prevent direct commits to `main` branch)
- Frontend CI pipeline
- Static code analysis (SonarQube)
- Integration tests

## System User Guide
This section explains how to navigate and use the Kanban Board system from a user perspective.

### 1. Dashboard / Main Board View
The main screen displays all tasks organized into three columns:
- Todo
- In Progress
- Done

Each task appears as a card showing:
title, description, created and last updated date & time, action buttons (edit / delete), drag handle

<img width="1896" height="706" alt="image" src="https://github.com/user-attachments/assets/debd8030-1ea9-4d80-97cb-dedad408f54e" />

### 2. Creating a New Task
- Click Add Task button
- A modal form opens
- Enter title (required), description (optional)
- Click Save
- Task is created in `Todo` column by default
- Appears immediately on the board

<img width="600" height="377" alt="image" src="https://github.com/user-attachments/assets/d4be07fe-e5b3-4f18-8534-680048de21e5" />

### 3. Editing a Task
- Click the Edit icon on a task card
- Modify title, description 
- Click Save
- Task is updated immediately
- Updated timestamp is refreshed

<img width="622" height="420" alt="image" src="https://github.com/user-attachments/assets/13271cd7-9335-434a-a6d9-1e1a41609213" />

### 4. Deleting a Task
- Click the Delete icon on a task
- Confirm deletion in the popup modal
- Task is removed from the board

<img width="627" height="426" alt="image" src="https://github.com/user-attachments/assets/3c36d3c2-d5a8-4583-bfb1-b560868845d4" />

<img width="562" height="166" alt="image" src="https://github.com/user-attachments/assets/50b5e18a-051a-46ba-b596-fafc3d5ad7de" />

### 5. Moving Tasks Between Columns
Tasks can be moved between statuses using the drag handle on the top right corner of the task card.
- Click and hold the drag handle on a task
- Drag task to another column
- Release to drop
- UI reflects new position immediately

<img width="693" height="427" alt="image" src="https://github.com/user-attachments/assets/dcdbf0ea-e838-433d-8a6a-40a21cfbd72a" />

### 6. Error Handling
If an operation fails or a validation rule is not met, an error message is displayed and no changes are applied.

<img width="566" height="382" alt="image" src="https://github.com/user-attachments/assets/5442e097-982b-4090-96d0-a5cdf4aed88f" />

