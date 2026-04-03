# Running QuantityMeasurementApp: Console vs API Mode Guide

## Overview
The application has been successfully split into two separate modes:
1. **Console Mode** - Interactive command-line interface using QuantityMeasurementConsole project
2. **API Mode** - RESTful web service using QuantityMeasurementApp project

Both modes share the same business logic libraries:
- QuantityMeasurementBusiness
- QuantityMeasurementController
- QuantityMeasurementModel
- QuantityMeasurementRepo

## Project Structure

```
src/
├── QuantityMeasurementApp/          (API Application)
│   ├── Controllers/                 (REST API endpoints)
│   ├── Middleware/                  (Exception handling, etc.)
│   ├── Authentication/              (JWT, Auth logic)
│   └── Program.cs                   (API entry point)
│
├── QuantityMeasurementConsole/      (Console Application) [NEW]
│   ├── UI/
│   │   ├── Menu.cs                  (Console menu interface)
│   │   └── IApplicationUI.cs        (UI contract)
│   └── Program.cs                   (Console entry point)
│
├── QuantityMeasurementBusiness/     (Shared)
├── QuantityMeasurementController/   (Shared)
├── QuantityMeasurementModel/        (Shared)
└── QuantityMeasurementRepo/         (Shared)
```

## How to Run Each Mode

### Console Mode

**Option 1: Using command-line argument**
```bash
cd src/QuantityMeasurementConsole
dotnet run -- --mode=console
```

**Option 2: Using environment variable**
```bash
set APP_MODE=console
cd src/QuantityMeasurementConsole
dotnet run
```

**Option 3: From solution root**
```bash
dotnet run --project src/QuantityMeasurementConsole/QuantityMeasurementConsole.csproj -- --mode=console
```

### API Mode

**Option 1: Using command-line argument**
```bash
cd src/QuantityMeasurementApp
dotnet run -- --mode=api
```

**Option 2: Using environment variable**
```bash
set APP_MODE=api
cd src/QuantityMeasurementApp
dotnet run
```

**Option 3: From solution root**
```bash
dotnet run --project src/QuantityMeasurementApp/QuantityMeasurementApp.csproj -- --mode=api
```

**Note:** If you run QuantityMeasurementApp without specifying `--mode=console`, it will default to `--mode=api` and run as a web service.

## Mode Detection Logic

Both applications include built-in mode detection:

### QuantityMeasurementApp (API)
- Default mode: `api`
- Detects mode from: Command-line arg `--mode=` or environment variable `APP_MODE`
- If mode is not `api`, exits with an error message

### QuantityMeasurementConsole
- Default mode: `console`
- Detects mode from: Command-line arg `--mode=` or environment variable `APP_MODE`
- If mode is not `console`, exits with an error message

## Configuration Files

Both applications use the same `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "QuantityMeasurementDb": "..."
  },
  "Jwt": {
    "Issuer": "...",
    "Audience": "...",
    "SecretKey": "...",
    "ExpirationMinutes": 60
  },
  "SeedUser": {
    "Username": "...",
    "Password": "...",
    "Role": "Admin"
  }
}
```

## Features by Mode

### Console Mode
- ✓ Interactive measurement type selection (Length, Weight, Volume, Temperature)
- ✓ Operations: Compare, Convert, Add, Subtract, Divide
- ✓ View operation history with formatted table
- ✓ Local database for storing operations
- ✗ No web server / no HTTP API

### API Mode
- ✓ RESTful endpoints for measurements
- ✓ JWT authentication
- ✓ Swagger/OpenAPI documentation (at `/swagger`)
- ✓ Async operations
- ✓ HTTP status codes and error handling
- ✗ No interactive user interface

## Shared Libraries

Both modes use identical implementations of:
- `QuantityMeasurementService` - Business logic for measurements
- `QuantityMeasurementController` - Controller logic (reused for both API and Console)
- Unit conversions and validations

## Database

Both modes use the same SQL Server database and will:
1. Create `UserCredentials` table if it doesn't exist
2. Seed default user if configured in `appsettings.json`
3. Store operation history in the same database

## Building for Production

### Build Console Application
```bash
dotnet publish src/QuantityMeasurementConsole/QuantityMeasurementConsole.csproj -c Release -o ./publish/console
```

### Build API Application
```bash
dotnet publish src/QuantityMeasurementApp/QuantityMeasurementApp.csproj -c Release -o ./publish/api
```

## Troubleshooting

### Error: "This is the Console application. Use '--mode=console' or set APP_MODE=console"
- You're running QuantityMeasurementConsole but with `--mode=api`
- Run with `--mode=console` or don't specify a mode (defaults to console)

### Error: "This is the API application. Use '--mode=api' or set APP_MODE=api"
- You're running QuantityMeasurementApp but with `--mode=console`
- Run with `--mode=api` or don't specify a mode (defaults to api)

### Database Connection Issues
- Ensure connection string in `appsettings.json` points to valid SQL Server
- Both apps use the same connection string
- Check `appsettings.json` exists in the working directory of each app

## Launching from Visual Studio

### Console Mode
1. Right-click on QuantityMeasurementConsole project
2. Select "Set as Startup Project"
3. Press F5 or Debug → Start Debugging

### API Mode
1. Right-click on QuantityMeasurementApp project
2. Select "Set as Startup Project"
3. Press F5 or Debug → Start Debugging (will open http://localhost:5000/swagger)

## Launch Profiles in launchSettings.json

Each application can have separate launch profiles configured:

**QuantityMeasurementConsole/Properties/launchSettings.json**
```json
{
  "profiles": {
    "Console": {
      "commandName": "Project",
      "commandLineArgs": "--mode=console"
    }
  }
}
```

**QuantityMeasurementApp/Properties/launchSettings.json**
```json
{
  "profiles": {
    "API": {
      "commandName": "Project",
      "commandLineArgs": "--mode=api"
    }
  }
}
```

## Future Enhancements

- Add Docker support for both modes
- Add Kubernetes deployment profiles
- Add configuration profiles for different environments
- Add background service mode for scheduled tasks
