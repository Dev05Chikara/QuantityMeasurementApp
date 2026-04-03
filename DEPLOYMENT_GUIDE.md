# Quantity Measurement App - Dual Mode Setup ✓

## Build Status: SUCCESS ✓
All 7 projects compiled successfully with **0 errors** and **0 warnings**.

---

## Project Structure

```
QuantityMeasurementApp/
├── src/
│   ├── QuantityMeasurementApp.Api/          ← REST API (port 5000)
│   ├── QuantityMeasurementApp.Console/      ← Interactive Console UI
│   ├── QuantityMeasurementApp.Business/     ← Shared business logic
│   ├── QuantityMeasurementApp.Models/       ← Shared DTOs & Enums
│   ├── QuantityMeasurementApp.Repository/   ← Data access layer
│   └── QuantityMeasurementController/       ← Shared controller logic
├── tests/
│   └── QuantityMeasurementApp.Tests/        ← NUnit test suite
└── QuantityMeasurementApp.sln
```

---

## How to Run

### Option 1: API Mode (REST API)
```powershell
cd src/QuantityMeasurementApp.Api
dotnet run --mode=api

# OR simply (defaults to API mode):
dotnet run
```
- **Access**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger
- **Default Port**: 5000

### Option 2: Console Mode (Interactive Menu)
```powershell
cd src/QuantityMeasurementApp.Console
dotnet run --mode=console

# OR simply (defaults to Console mode):
dotnet run
```
- **Interactive**: Menu-driven command-line interface
- **Features**: Quantity comparison, conversion, arithmetic

---

## Mode Detection

Both applications automatically detect the running mode:

1. **Command-line argument** (highest priority):
   ```
   dotnet run --mode=console
   dotnet run --mode=api
   ```

2. **Environment variable** (fallback):
   ```
   $env:APP_MODE = "console"
   $env:APP_MODE = "api"
   ```

3. **Default** (if neither above):
   - API app → defaults to `api` mode
   - Console app → defaults to `console` mode

---

## Configuration

Both apps share the same database configuration via `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "QuantityMeasurementDb": "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "SecretKey": "QuantityMeasurementAppSuperSecretKey2026!",
    "ExpiryMinutes": 60
  },
  "SeedUser": {
    "Username": "admin",
    "Password": "admin123"
  }
}
```

**Database**: SQL Server (LocalDB or SQLEXPRESS)  
**Auto-initialization**: Database tables created on first run

---

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Get JWT token

### Measurements
- `POST /api/measurements/compare` - Compare quantities
- `POST /api/measurements/convert` - Convert units
- `POST /api/measurements/add` - Add quantities
- `GET /api/measurements/history` - View operation history

---

## Console Features

- **Interactive Menu System**
  - Compare quantities
  - Convert between units
  - Perform arithmetic (add, subtract, multiply)
  - View operation history

- **Supported Units**
  - **Length**: mm, cm, m, km, inch, foot, yard, mile
  - **Weight**: mg, g, kg, ton, pound, ounce
  - **Volume**: ml, l, gallon, pint
  - **Temperature**: Celsius, Fahrenheit, Kelvin

---

## Technology Stack

- **.NET 10.0** with ASP.NET Core
- **Entity Framework Core 9.0.10** - ORM
- **SQL Server** - Database
- **JWT Bearer** - API Authentication
- **Swagger/OpenAPI** - API Documentation
- **NUnit** - Testing Framework
- **Minimal APIs** - ASP.NET Core pattern

---

## Key Features ✓

✓ **Dual Entry Points** - Use via API or Console  
✓ **Shared Business Logic** - Both modes share same computation engine  
✓ **Mode Detection** - Automatic or explicit mode selection  
✓ **JWT Authentication** - Secure API access  
✓ **Database Persistence** - Operation history stored  
✓ **Swagger Documentation** - API endpoint reference  
✓ **Interactive Console UI** - Menu-driven experience  
✓ **Comprehensive Unit Support** - 15+ unit types  

---

## Next Steps

1. **Run API**: `dotnet run` in `src/QuantityMeasurementApp.Api`
2. **Run Console**: `dotnet run` in `src/QuantityMeasurementApp.Console`
3. **Test Both**: Verify calculations work identically in both modes
4. **Deploy**: Use `dotnet publish -c Release` for production builds

---

**Status**: ✅ All projects compiled successfully. Both API and Console modes are ready to use!
