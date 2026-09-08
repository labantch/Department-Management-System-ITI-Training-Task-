# 🎓 Department Management System (ITI Task)

A full-stack, N-Tier web application built with **ASP.NET Core MVC** and **Entity Framework Core**. This project serves as an administrative portal to manage educational institute operations, featuring a modern, responsive UI built with custom Tailwind CSS inspired by Material Design 3.

## ✨ Key Features

* **Executive Dashboard:** A Bento-grid dashboard displaying live statistics for departments, courses, instructors, and enrollments.
* **Curriculum Management:** Full CRUD operations for Departments and Course tracks (including constraints like minimum passing degrees).
* **Faculty Directory:** Instructor management linked directly to specific departments and course tracks.
* **Admissions & Enrollments:** Register new trainees and assign them to active learning tracks with status tracking.
* **Clean UI/UX:** Responsive design utilizing glass-morphism, elevation shadows, and Google Material Symbols.

## 🏗️ Architecture (N-Tier)

The solution is divided into three distinct layers to ensure separation of concerns:

1. **PresentationLayer (ASP.NET Core MVC):** Controllers, Razor Views, and UI assets.
2. **BLogicLayer (Business Logic):** Service interfaces and implementations enforcing business rules.
3. **DataAccessLayer:** Entity Framework Core `DbContext` and domain models (`Course`, `Department`, `Instructor`, `Enrollment`).

---

## 🚀 Getting Started

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (or your targeted version)
* SQL Server (or SQL Server Express)
* Visual Studio 2022

### 1. Clone the Repository

```bash
git clone https://github.com/labantch/Department-Management-System-Simple-ITI-Task-.git
cd "Department Management System"

```

### 2. 🗄️ Database Setup (Important)

This project uses Entity Framework Core with SQL Server. The database connection is configured inside the `DataAccessLayer` in your `DbContext` class.

By default, it targets a local default SQL Server instance:

```csharp
optionsBuilder.UseSqlServer("Server=.;Database=ITI;Trusted_Connection=True; TrustServerCertificate=true;");

```

**Note:** If you are using **SQL Server Express**, you must open your DbContext file and change `Server=.;` to `Server=.\SQLEXPRESS;` before running migrations.

### 3. Apply Migrations

Open the **Package Manager Console** in Visual Studio, ensure the `DataAccessLayer` is set as the Default Project, and run:

```powershell
Update-Database

```

### 4. Run the Application

Set `PresentationLayer` as your Startup Project in Visual Studio and press `F5` to launch the dashboard.
