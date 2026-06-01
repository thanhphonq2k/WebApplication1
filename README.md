# Employee Management API

REST API quản lý **nhân viên (Employee)** và **phòng ban (Department)**, hỗ trợ upload ảnh. Xây dựng bằng **ASP.NET Core 3.1 Web API** và **SQL Server**.

## Tech stack

| | |
|---|---|
| Framework | ASP.NET Core 3.1 |
| Database | SQL Server (`Microsoft.Data.SqlClient`) |
| JSON | Newtonsoft.Json (PascalCase) |
| API docs | Swagger / OpenAPI (Development) |

## Project structure

```text
WebApplication1/
├── WebApplication1.sln
├── README.md
└── WebApplication1/
    ├── Controllers/
    │   ├── EmployeeController.cs
    │   ├── DepartmentController.cs
    │   └── WeatherForecastController.cs   # template demo
    ├── Models/
    │   ├── Employee.cs
    │   └── Department.cs
    ├── Photos/                            # uploaded images
    ├── Properties/launchSettings.json
    ├── appsettings.json
    ├── Startup.cs
    └── Program.cs
```

## Features

- CRUD `/api/department`
- CRUD `/api/employee`
- Upload ảnh: `POST /api/employee/savefile`
- Static files: `GET /Photos/{filename}`
- CORS enabled (development)
- Swagger UI at `/swagger`

## Requirements

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1) or newer SDK
- SQL Server (Express / LocalDB / full)
- Visual Studio 2022 or `dotnet` CLI

## Database setup

```sql
CREATE DATABASE mytestdb;
GO
USE mytestdb;
GO

CREATE TABLE dbo.Department (
    DepartmentId   INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL
);

CREATE TABLE dbo.Employee (
    EmployeeId     INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeName   NVARCHAR(100) NOT NULL,
    Department     NVARCHAR(100) NULL,
    DateOfJoining  DATE NULL,
    PhotoFileName  NVARCHAR(255) NULL
);
```

`Employee.Department` stores the **department name** (string), not `DepartmentId`.

## Configuration

Edit `WebApplication1/appsettings.json`:

```json
"ConnectionStrings": {
  "EmployeeAppCon": "Data Source=YOUR_SERVER;Initial Catalog=mytestdb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
}
```

Use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) locally — do not commit real passwords.

Create the upload folder:

```bash
mkdir WebApplication1/Photos
```

## Run

### Visual Studio

1. Open `WebApplication1.sln`
2. Profile: **IIS Express** or **WebApplication1**
3. **F5**

| Profile | Base URL | Swagger |
|---------|----------|---------|
| IIS Express | `https://localhost:44395` | `/swagger` |
| Kestrel | `https://localhost:5001` | `/swagger` |

### CLI

```bash
cd WebApplication1
dotnet restore
dotnet run
```

## API endpoints

### Employee

| Method | URL | Body / notes |
|--------|-----|----------------|
| GET | `/api/employee` | — |
| POST | `/api/employee` | JSON: `employeeName`, `department`, `dateOfJoining`, `photoFileName` |
| PUT | `/api/employee` | Include `employeeId` |
| DELETE | `/api/employee/{id}` | — |
| POST | `/api/employee/savefile` | `multipart/form-data`, field name `file` |

### Department

| Method | URL | Body / notes |
|--------|-----|----------------|
| GET | `/api/department` | — |
| POST | `/api/department` | `{ "departmentName": "..." }` |
| PUT | `/api/department` | `{ "departmentId", "departmentName" }` |
| DELETE | `/api/department/{id}` | — |

### Example (create employee)

```json
POST /api/employee
{
  "employeeName": "Nguyen Van A",
  "department": "IT",
  "dateOfJoining": "2024-01-15",
  "photoFileName": "photo.jpg"
}
```

## Test with Swagger

1. Run the API in **Development**
2. Open `https://localhost:44395/swagger` (or port from your profile)
3. Expand an endpoint → **Try it out** → **Execute**

## Architecture

Single-project Web API: controllers execute SQL directly (no separate Application/Domain layers).

```text
HTTP Client  →  Controllers  →  SQL Server
                    ↓
                 Photos/
```

## Troubleshooting

| Problem | Solution |
|---------|----------|
| `PlatformNotSupportedException` (SqlClient) | Project uses `Microsoft.Data.SqlClient` |
| Build: DLL file locked | Stop IIS Express, then rebuild |
| SQL connection failed | Check connection string and run DB script |
| Upload error | Ensure `Photos/` folder exists |
| Swagger not shown | `ASPNETCORE_ENVIRONMENT=Development` |

## Git

Suggested `.gitignore`: `bin/`, `obj/`, `.vs/`, `Photos/*` (except `.gitkeep`), secrets in config.

## License

Learning / internal use — add a `LICENSE` file if needed.
