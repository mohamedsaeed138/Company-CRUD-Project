# Company - MVC CRUD Project

## Description

Company is my first MVC web project developed as a CRUD application for practicing ASP.NET Core 8, EF Core, and Microsoft SQL Server skills acquired during an internship at The ITI (Institute in Egypt).

## Installation Instructions

### Prerequisites
- .NET 8 SDK: Download and install the .NET 8 SDK from the [official website](https://dotnet.microsoft.com/download).
- Microsoft SQL Server: Ensure you have Microsoft SQL Server installed and running. You can download it from the [official website](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

### Clone the Repository

```sh
git clone https://github.com/mohamedsaeed138/Company-CRUD-Project.git
cd Company
```

### Set Up the Database

- Update the connection string in appsettings.json.

- Apply migrations to create the database schema.  

```sh
dotnet ef database update
```

### Build and Run the Project

```sh
dotnet build
dotnet run
```

## Features

CRUD operations for Employees, Departments, Department Locations, Projects, and Employees' Dependents.

## Technologies/Frameworks Used

- ASP.NET Core 8
- LINQ
- EF Core
- Microsoft SQL Server

## Screenshots

![Create Department](https://github.com/mohamedsaeed138/Company-CRUD-Project/blob/master/Screenshots/Create%20Department.png?raw=true)
![Departments](https://github.com/mohamedsaeed138/Company-CRUD-Project/blob/master/Screenshots/Departmen.png?raw=true)
![Departments Locations](https://github.com/mohamedsaeed138/Company-CRUD-Project/blob/master/Screenshots/Departmens%20Locations.png?raw=true)

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/mohamedsaeed138/Company-CRUD-Project/blob/master/LICENSE.txt) file for details.
