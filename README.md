# Bank Console Application

This is a simple banking project that I built with C# and Entity Framework Core.

The main purpose of this project was to practice working with EF Core, LINQ, repositories, services and business rules in a real project structure.

The application currently supports creating cards, card authentication, money transfer and viewing sent and received transactions.

For authentication, the application keeps track of wrong password attempts and can deactivate a card after multiple failed attempts.

For money transfer, the application checks things like card status, transfer amount and available balance before completing the transaction.

The project is separated into a few layers:

- `Bank.Domain` contains entities, DTOs, interfaces and custom exceptions.
- `Bank.Application` contains the services and application logic.
- `Bank.Infrastructure` contains EF Core, repositories, configurations and migrations.
- `Bank.Presentation` contains the console application and user interaction.

Some of the main things I practiced in this project were:

- Entity Framework Core
- LINQ
- Repository Pattern
- Service Layer
- Change Tracking
- DTOs
- Entity relationships
- Fluent API
- Custom Exceptions
- Business Rules

The project uses SQL Server as the database.

Before running the project, the connection string can be changed in:

`Bank.Infrastructure/Data/AppDbContext.cs`

After that, migrations can be applied with:

```powershell
Update-Database
