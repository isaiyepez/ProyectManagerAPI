Aquí está tu README traducido al inglés:

---

# 📋 Project Management REST API
RESTful API developed with **.NET 8** for project and task management. This solution implements a layered architecture (N-Layer Architecture) focused on separation of concerns, scalability, and robust error handling.

## 🚀 Technologies
* **Core:** .NET 8 SDK
* **ORM:** Entity Framework Core
* **Database:** SQL Server (Compatible with InMemory/Postgres)
* **Error Format:** ProblemDetails (RFC 7807)

### Project Breakdown
1. **ProjectManagementRestAPI (Web Layer)**
* The entry point of the application.
* **Controllers (`ProjectController`, `TaskItemController`):** Implement the "Skinny Controller" pattern. They don't contain business logic; they only orchestrate the HTTP request and delegate to the service.
* **Middleware (`GlobalExceptionHandler`):** Centralizes error handling. It captures exceptions like `KeyNotFoundException` or `InvalidOperationException` and converts them into standardized HTTP responses (404, 400).

2. **BusinessLogic (Service Layer)**
* The "brain" of the application. Contains `ProjectService` and `TaskItemService`.
* Business validation rules reside here (e.g., "A project with pending tasks cannot be completed").
* Communicates with the data layer through interfaces (Dependency Injection).

3. **Data (Persistence Layer)**
* Contains the `ApplicationDbContext` and the Repository pattern implementation (`ProjectRepository`, `TaskItemRepository`).
* Abstracts database access using Entity Framework Core.

4. **Models (Domain Layer)**
* Contains pure entities (`Project`, `TaskItem`) and Enumerations (`StatusProject`, `StatusTask`, `PriorityTask`).
* Represents the database schema.

5. **DTOs (Data Transfer Objects)**
* Flat objects used to transfer data between the client and server.
* Allows decoupling database entities from the public API and applying input validations (`[Required]`, `[StringLength]`).

## 💡 Key Technical Decisions
### 1. Global Exception Handling
Instead of using repetitive `try-catch` blocks in each controller, a `GlobalExceptionHandler` was implemented.
* **Benefit:** Cleaner code and consistent error responses under the **ProblemDetails** standard.
* If a resource doesn't exist, the service throws an exception and the handler automatically returns a **404 Not Found**.

### 2. Repository & Service Pattern
Data access was separated from business logic.
* **Repository:** Responsible only for interacting with the DB (CRUD).
* **Service:** Orchestrates business rules and validations before calling the repository.

### 3. Domain Validations
Non-trivial rules are validated in the Service, not in the Controller or Database.
* *Example:* Validating that `DueDate` is not a past date or preventing deletion of an active project.

## ✅ Implemented Business Rules
**Projects (`Project`):**
* A project cannot be deleted if it has pending or in-progress tasks.
* Cannot be marked as `Finished` if it has incomplete tasks.
* Automatic calculation of progress percentage.

**Tasks (`TaskItem`):**
* **Date Integrity:** Tasks cannot be created with `DueDate` in the past.
* **State Flow:** A task cannot jump directly from `Pending` to `Completed`; it must go through `InProgress`.
* **Queries:** Filtering by priority and detection of overdue tasks.

## 🛠 Installation and Execution
1. **Clone the repository:**
```bash
git clone https://github.com/your-username/ProjectManagerAPI.git
```

2. **Configure Database:**
Make sure you have the connection string configured in `appsettings.json`.

3. **Apply Migrations:**
From the terminal in the `Data` project folder or root:
```bash
dotnet ef database update --project Data --startup-project ProjectManagementRestAPI
```

4. **Run:**
```bash
dotnet run --project ProjectManagementRestAPI
```
