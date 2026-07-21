# Digital Nurture: .NET & Angular FSE Solutions

This repository contains the completed, clean solutions for the **Digital Nurture Full Stack Engineering (.NET & Angular)** program. It is divided into two main tracks: **Deepskilling** and **Upskilling**.

---

## 📂 Repository Structure

### 1. 🚀 Deepskilling
Advanced programming, architecture, frameworks, and database topics:

*   **[Advanced SQL Server](Deepskilling/Advanced%20SQL%20server/Solutions)**: 8 custom script files covering window functions, grouping, indexes, views, stored procedures, triggers, cursors, and custom exception handling.
*   **[Angular](Deepskilling/Angular/student-course-portal)**: Standalone Angular 20 application with services, forms (template and reactive with sync/async custom validations), routing guards, credit pipes, and NgRx store.
*   **[Engineering Concepts](Deepskilling/Engineering%20concepts)**:
    *   **DSA**: 7 C# algorithms and data structures exercises.
    *   **Design Patterns**: 11 classic patterns (Singleton, Factory, Builder, Adapter, Decorator, etc.) implemented in a compiled C# console project.
*   **[Entity Framework Core](Deepskilling/Entity%20Framework%20Core/Solutions)**: C# console project using EF Core 8.0 for category and product management.
*   **[GIT](Deepskilling/GIT/Solutions)**: Walkthrough and commands for all 5 Git-HOL labs.
*   **[Microservices](Deepskilling/Microservices/Solutions)**: Kafka messaging console apps and JWT Auth REST API middleware.
*   **[NUnit and Moq](Deepskilling/NUnit%20and%20Moq/Solutions)**: Test suites validating library outputs and mock mail services.
*   **[React](Deepskilling/React/Solutions)**: 16 separate React applications covering hooks, states, rendering lists, and form inputs.
*   **[WebApi](Deepskilling/WebApi/Solutions)**: 5 Web API projects with custom routing and action filters.

---

### 2. 📈 Upskilling
Core concepts and foundation modules:

*   **[Module 1 (Web Portal)](Upskilling/Solutions/Module1_Portal)**: A responsive and interactive **Local Community Event Portal** built using HTML5, CSS3, Bootstrap 5, Javascript, and jQuery. Includes:
    *   Dynamic category filtering and event list rendering.
    *   Interactive jQuery sandbox and image gallery scaling.
    *   Form validations, localStorage preferences, and Geolocation mapping.
*   **[Module 2 (MySQL)](Upskilling/Solutions/Module2_MySQL)**: SQL script compiling database schemas, inserts, and query solutions for 25 MySQL exercises.
*   **[Module 3 (C# ADO.NET)](Upskilling/Solutions/Module3_CS_ADO_NET)**: Compiled C# console project featuring 30 foundational C# exercises (records, value/reference variables, streams, threads, and ADO.NET CRUD templates).

---

## 🛠️ How to Use

### Database Scripts
SQL scripts in **Advanced SQL Server** and **Module 2 (MySQL)** can be executed directly in SQL Server Management Studio (SSMS) or any MySQL client.

### C# and .NET Projects
For EF Core, Design Patterns, Web API, and ADO.NET console apps:
```bash
dotnet restore
dotnet build
dotnet run
```

### Angular Portal
Ensure you have Node.js installed, navigate to the portal directory, install dependencies, and run:
```bash
npm install
npm run start
```
