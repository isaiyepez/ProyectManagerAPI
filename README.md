
# 📋 Project Management REST API

API RESTful desarrollada con **.NET 8** para la gestión de proyectos y tareas. Esta solución implementa una arquitectura en capas (N-Layer Architecture) enfocada en la separación de responsabilidades, escalabilidad y un manejo robusto de errores.

## 🚀 Tecnologías

* **Core:** .NET 8 SDK
* **ORM:** Entity Framework Core
* **Base de Datos:** SQL Server (Compatible con InMemory/Postgres)
* **Formato de Errores:** ProblemDetails (RFC 7807)

### Desglose de Proyectos

1. **ProjectManagementRestAPI (Web Layer)**
* Es el punto de entrada de la aplicación.
* **Controllers (`ProjectController`, `TaskItemController`):** Implementan el patrón "Skinny Controller". No contienen lógica de negocio; solo orquestan la petición HTTP y delegan al servicio.
* **Middleware (`GlobalExceptionHandler`):** Centraliza el manejo de errores. Captura excepciones como `KeyNotFoundException` o `InvalidOperationException` y las convierte en respuestas HTTP estandarizadas (404, 400).


2. **BusinessLogic (Service Layer)**
* El "cerebro" de la aplicación. Contiene `ProjectService` y `TaskItemService`.
* Aquí residen las validaciones de negocio (ej. "No se puede finalizar un proyecto con tareas pendientes").
* Se comunica con la capa de datos mediante interfaces (Inyección de Dependencias).


3. **Data (Persistence Layer)**
* Contiene el `ApplicationDbContext` y la implementación del patrón Repositorio (`ProjectRepository`, `TaskItemRepository`).
* Abstrae el acceso a la base de datos usando Entity Framework Core.


4. **Models (Domain Layer)**
* Contiene las entidades puras (`Project`, `TaskItem`) y los Enumerados (`StatusProject`, `StatusTask`, `PriorityTask`).
* Representa el esquema de la base de datos.


5. **DTOs (Data Transfer Objects)**
* Objetos planos utilizados para transportar datos entre el cliente y el servidor.
* Permite desacoplar las entidades de base de datos de la API pública y aplicar validaciones de entrada (`[Required]`, `[StringLength]`).



## 💡 Decisiones Técnicas Clave

### 1. Manejo Global de Excepciones

En lugar de usar bloques `try-catch` repetitivos en cada controlador, se implementó un `GlobalExceptionHandler`.

* **Beneficio:** Código más limpio y respuestas de error consistentes bajo el estándar **ProblemDetails**.
* Si un recurso no existe, el servicio lanza una excepción y el handler devuelve automáticamente un **404 Not Found**.

### 2. Patrón Repository & Service

Se separó el acceso a datos de la lógica de negocio.

* **Repository:** Se encarga solo de interactuar con la DB (CRUD).
* **Service:** Orquesta las reglas de negocio y validaciones antes de llamar al repositorio.

### 3. Validaciones de Dominio

Las reglas no triviales se validan en el Servicio, no en el Controlador ni en la Base de Datos.

* *Ejemplo:* Validar que `DueDate` no sea una fecha pasada o impedir el borrado de un proyecto activo.

## ✅ Reglas de Negocio Implementadas

**Proyectos (`Project`):**

* No se puede eliminar un proyecto si tiene tareas pendientes o en progreso.
* No se puede marcar como `Finished` si tiene tareas incompletas.
* Cálculo automático del porcentaje de progreso.

**Tareas (`TaskItem`):**

* **Integridad de Fechas:** No se pueden crear tareas con `DueDate` en el pasado.
* **Flujo de Estados:** Una tarea no puede saltar directamente de `Pending` a `Completed`; debe pasar por `InProgress`.
* **Consultas:** Filtrado por prioridad y detección de tareas vencidas.

## 🛠 Instalación y Ejecución

1. **Clonar el repositorio:**
```bash
git clone https://github.com/tu-usuario/ProjectManagerAPI.git

```


2. **Configurar Base de Datos:**
Asegúrate de tener la cadena de conexión configurada en `appsettings.json`.
3. **Aplicar Migraciones:**
Desde la terminal en la carpeta del proyecto `Data` o raíz:
```bash
dotnet ef database update --project Data --startup-project ProjectManagementRestAPI

```


4. **Ejecutar:**
```bash
dotnet run --project ProjectManagementRestAPI

```
---

**Autor:** Brian Covarrubias
**Licencia:** MIT
