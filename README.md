# TaskFlow - Task Management System

TaskFlow is a modern task management web application developed with ASP.NET Core MVC and SQL Server. The system enables users to manage tasks efficiently while providing administrators with tools to monitor users and task activity.

## 🚀 Features

### User Authentication

* User registration
* Secure login and logout
* Session-based authentication
* Account management

### Task Management

* Create tasks
* Edit tasks
* Delete tasks
* View personal tasks
* Task assignment
* Task status management
* Task history tracking

### User Dashboard

* Personal task overview
* Assigned task management
* Task progress tracking

### Admin Dashboard

* View all users
* Delete user accounts
* View all tasks
* Create tasks
* Delete any task
* Monitor overall system activity

## 🛠 Tech Stack

* **Framework:** ASP.NET Core 8 MVC
* **Language:** C#
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Frontend:** Razor Views, HTML, CSS, JavaScript
* **Architecture:** MVC (Model–View–Controller)

## ⚙️ Configuration

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio 2022

### Setup

1. Clone the repository

```bash
git clone https://github.com/NihatKarim/TaskFlow.git
cd TaskFlow
```

2. Configure your connection string in `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
  }
}
```

3. Restore NuGet packages

```bash
dotnet restore
```

4. Apply database migrations

```bash
dotnet ef database update
```

5. Run the application

```bash
dotnet run
```

## 📂 Project Structure

* Models
* Views
* Controllers
* Data
* Migrations
* wwwroot

## 🔐 Security

* Session-based authentication
* HTTP-only cookies
* Authentication timeout
* HTTPS support

## 📈 Future Improvements

* Email verification
* Task notifications
* File attachments
* Search and filtering
* Dashboard analytics
* Role-based permissions enhancement

## 👨‍💻 Author

**Nihat Karimi**

Computer Engineering Graduate
National Aviation Academy

GitHub: https://github.com/NihatKarim

## 📄 License

This project was developed for educational purposes as a Final Graduation Project.
