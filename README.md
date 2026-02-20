### Made this using AI since it could cover everything that I may have missed in my code to get it running for a code review

# MVC Elden Ring Boss Lore

An ASP.NET Core MVC application for exploring Elden Ring boss lore.

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- MySQL Server (local or Azure)
- Git

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <your-repo-url>
cd MvcEldenRingBossLore
```

### 2. Configure User Secrets

This project uses **User Secrets** to keep database credentials secure. You must configure your own secrets before running the application.

#### Initialize User Secrets (if not already done)

```bash
dotnet user-secrets init
```

#### Set Your MySQL Connection String

```bash
# Set base connection string (without credentials)
dotnet user-secrets set "ConnectionStrings:MySqlBase" "server=localhost;port=3306;database=MvcEldenRingBossLore"

# Set database credentials separately
dotnet user-secrets set "DbUser" "your_mysql_username"
dotnet user-secrets set "DbPassword" "your_mysql_password"
```

Replace `your_mysql_username` and `your_mysql_password` with your actual MySQL credentials.

**Note:** Separating credentials from the base connection string allows different developers to use their own credentials while sharing the same connection configuration.

#### Verify Your Secrets

```bash
dotnet user-secrets list
```

### 3. Create the Database

Make sure MySQL is running, then create the database:

```bash
# For macOS/Linux with MySQL installed via Homebrew
brew services start mysql

# Or start MySQL server manually
mysql.server start

# Create the database
mysql -u root -p -e "CREATE DATABASE MvcEldenRingBossLore;"
```

### 4. Apply Migrations

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Production Deployment (Azure)

For Azure deployment, update `appsettings.Production.json` with your Azure MySQL connection details:

```json
{
  "ConnectionStrings": {
    "MySqlBase": "server=YOUR-SERVER.mysql.database.azure.com;port=3306;database=MvcEldenRingBossLore;sslmode=Required"
  },
  "DbUser": "YOUR-USERNAME",
  "DbPassword": "YOUR-PASSWORD"
}
```

**Note:** For production, consider using Azure Key Vault or environment variables instead of storing credentials directly in configuration files.

## Project Structure

- **Controllers/** - MVC controllers
- **Models/** - Data models and Entity Framework entities
- **Views/** - Razor view templates
- **Data/** - Entity Framework database context
- **Migrations/** - Database migration files
- **wwwroot/** - Static files (CSS, JS, images)

## Technologies Used

- ASP.NET Core 9.0 MVC
- Entity Framework Core 9.0
- MySQL (via Pomelo.EntityFrameworkCore.MySql)
- Bootstrap 5

## Troubleshooting

### Connection Issues

If you experience connection issues:
1. Verify MySQL is running: `mysql.server status`
2. Check your user secrets are set correctly: `dotnet user-secrets list`
3. Ensure the database exists: `mysql -u root -p -e "SHOW DATABASES;"`

### Migration Issues

To reset migrations:
```bash
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## License

Created by Nicholas Maddox
