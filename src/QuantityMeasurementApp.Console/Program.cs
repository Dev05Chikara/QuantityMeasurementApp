using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Controller;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Repository.Implementations;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementConsole.UI;

// Get mode from command line arguments or environment variable
var mode = args.FirstOrDefault(arg => arg.StartsWith("--mode="))?.Replace("--mode=", "")
    ?? Environment.GetEnvironmentVariable("APP_MODE")
    ?? "console";

if (mode != "console")
{
    Console.WriteLine("Error: This is the Console application. Use '--mode=console' or set APP_MODE=console");
    Console.WriteLine("For API mode, run the API application instead.");
    Environment.Exit(1);
}

// Setup Dependency Injection
var services = new ServiceCollection();

// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

services.AddSingleton(configuration);

// Database
services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("QuantityMeasurementDb")));

// Repositories
services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
services.AddScoped<IUserCredentialRepository, UserCredentialEfRepository>();

// Business Services
services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

// Controller
services.AddScoped<QuantityMeasurementController>();

// UI
services.AddScoped<IApplicationUI, Menu>();

var serviceProvider = services.BuildServiceProvider();

// Initialize database
try
{
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<QuantityMeasurementDbContext>();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserCredentialRepository>();

        // Create tables if they don't exist
        dbContext.Database.ExecuteSqlRaw(@"
IF OBJECT_ID('dbo.UserCredentials', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.UserCredentials
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        Role NVARCHAR(50) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_UserCredentials_IsActive DEFAULT 1,
        CreatedAtUtc DATETIME2 NOT NULL CONSTRAINT DF_UserCredentials_CreatedAtUtc DEFAULT SYSUTCDATETIME()
    );
END");

        // Seed default user if configured
        var seedUsername = configuration["SeedUser:Username"];
        var seedPassword = configuration["SeedUser:Password"];
        var seedRole = configuration["SeedUser:Role"] ?? "User";

        if (!string.IsNullOrWhiteSpace(seedUsername)
            && !string.IsNullOrWhiteSpace(seedPassword)
            && !userRepository.Exists(seedUsername))
        {
            userRepository.Add(new QuantityMeasurementApp.Repository.Models.UserCredentialRecord
            {
                Username = seedUsername,
                PasswordHash = QuantityMeasurementApp.Repository.PasswordHasher.Hash(seedPassword),
                Role = seedRole,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            });
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Warning: Database initialization issue: {ex.Message}");
}

// Run Console Application
try
{
    using (var scope = serviceProvider.CreateScope())
    {
        var ui = scope.ServiceProvider.GetRequiredService<IApplicationUI>();
        ui.Run();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Fatal Error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    Environment.Exit(1);
}
finally
{
    await serviceProvider.DisposeAsync();
}






