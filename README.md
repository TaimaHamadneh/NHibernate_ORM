# NHibernate Wallet Management Example

This project demonstrates basic CRUD operations (Create, Read, Update, Delete) and transaction handling using NHibernate in C#. It simulates a simple wallet system where users can add, update, delete, transfer, and retrieve wallet records stored in a SQL Server database.

---

## 📋 Features
- Insert (Create): Add new wallets to the database.
- Retrieve (Read): Fetch wallets (all or by ID).
- Update: Modify existing wallet balances.
- Delete: Remove wallet records.
- Transfer: Perform balance transfers between wallets within a transaction.

  ---

## Technologies Used

- **C# (.NET Core / .NET 6+)**
- **NHibernate ORM**
- **Microsoft SQL Server**
- **Microsoft.Extensions.Configuration for reading connection strings**
- **appsettings.json for configuration management**
  
---

## How to Clone & Run

- Ensure SQL Server is running and accessible.
- Update the connection string in appsettings.json.
- Build and run the project using:
 ```bash
dotnet run


