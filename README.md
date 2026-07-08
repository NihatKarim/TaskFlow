# TaskFlow

A modern **Task Management System** built with **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server**. The application enables users to create, manage, and track tasks while providing administrators with comprehensive task and user management features.

---

## Features

### User
- User registration and login
- Session-based authentication
- Create, edit, and delete tasks
- View personal tasks
- Track task history
- Manage assigned tasks

### Admin
- View all users
- Delete user accounts
- View all tasks
- Create tasks
- Delete any task
- Monitor system activity

---

## Tech Stack

- ASP.NET Core 8 MVC
- C#
- Entity Framework Core
- SQL Server
- Razor Views
- HTML5
- CSS3
- JavaScript

---

## Getting Started

### Requirements

- .NET 8 SDK
- SQL Server
- Visual Studio 2022

### Installation

```bash
git clone https://github.com/NihatKarim/TaskFlow.git
cd TaskFlow
```

Configure your SQL Server connection string inside **appsettings.json**.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

Run the following commands:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

---

## Project Structure

```
TaskFlow
│
├── Controllers
├── Data
├── Migrations
├── Models
├── Views
├── wwwroot
├── Program.cs
└── appsettings.json
```

---

## Security

- Session-based authentication
- HTTP-only cookies
- HTTPS support

---

## Future Improvements

- Email verification
- Notifications
- File attachments
- Advanced filtering and search
- Dashboard analytics
- Role-based authorization improvements

---

## Author

**Nihat Karimi**

Computer Engineering Graduate  
National Aviation Academy

GitHub: https://github.com/NihatKarim

---

## Project Status

✅ Completed as a Final Graduation Project.
