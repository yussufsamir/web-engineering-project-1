# Fitness Tracker API

##  Overview
This project is a backend API for a fitness tracking system built using ASP.NET Core.  
It supports athletes and coaches, allowing workout creation (WODs), assignment, and tracking.

The system implements authentication, authorization, DTO validation, and optimized database queries using Entity Framework Core.

---

##  Features

- Athlete signup and login
- Coach signup and login
- JWT authentication
- Role-based authorization (Athlete / Coach / Admin)
- Workout (WOD) management
- Assignment system
- Coach dashboard
- Password hashing using BCrypt
- DTO-based architecture (Create / Update / Read)
- Entity relationships (One-to-One, One-to-Many, Many-to-Many)
- Async database operations
- Optimized queries using LINQ Select and AsNoTracking()
- Postman API testing

---

##  Technologies Used

- **ASP.NET Core Web API**  
  Used to build the backend RESTful API.

- **Entity Framework Core**  
  ORM used for database interaction and managing relationships.

- **SQL Server (Docker)**  
  Relational database used to store application data.

- **JWT (JSON Web Tokens)**  
  Used for authentication and securing API endpoints.

- **BCrypt**  
  Used for hashing passwords securely before storing them.

- **Postman**  
  Used for testing and documenting API endpoints.

---

## How to Run the Project

1. Start SQL Server using Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Fitness!1234" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
