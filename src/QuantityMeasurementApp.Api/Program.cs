using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Middleware;
using QuantityMeasurementApp.Authentication;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Repository.Implementations;
using QuantityMeasurementApp.Repository.Interfaces;

// Get mode from command line arguments or environment variable
var mode = args.FirstOrDefault(arg => arg.StartsWith("--mode="))?.Replace("--mode=", "")
    ?? Environment.GetEnvironmentVariable("APP_MODE")
    ?? "api";

if (mode != "api")
{
    Console.WriteLine("Error: This is the API application. Use '--mode=api' or set APP_MODE=api");
    Console.WriteLine("For Console mode, run the Console application instead.");
    Environment.Exit(1);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keep PascalCase names
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);
var jwtOptions = jwtSection.Get<JwtOptions>() ?? new JwtOptions();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Add CORS to allow frontend requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "QuantityMeasurement API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token without Bearer prefix."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuantityMeasurementDb")));

builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
builder.Services.AddScoped<IUserCredentialRepository, UserCredentialEfRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QuantityMeasurementDbContext>();
    var userRepository = scope.ServiceProvider.GetRequiredService<IUserCredentialRepository>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

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

    // Create or update QuantityMeasurementHistory table
    dbContext.Database.ExecuteSqlRaw(@"
IF OBJECT_ID('dbo.QuantityMeasurementHistory', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.QuantityMeasurementHistory
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL DEFAULT 'Anonymous',
        Operation NVARCHAR(50) NOT NULL,
        Operand1Value FLOAT NOT NULL,
        Operand1UnitName NVARCHAR(50) NOT NULL,
        Operand1MeasurementType NVARCHAR(50) NOT NULL,
        Operand2Value FLOAT,
        Operand2UnitName NVARCHAR(50),
        Operand2MeasurementType NVARCHAR(50),
        ResultValue FLOAT,
        ResultUnitName NVARCHAR(50),
        ResultMeasurementType NVARCHAR(50),
        ErrorMessage NVARCHAR(MAX),
        CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );
END");

    // Add Username column if it doesn't exist (for existing databases)
    dbContext.Database.ExecuteSqlRaw(@"
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'QuantityMeasurementHistory' AND COLUMN_NAME = 'Username')
BEGIN
    ALTER TABLE dbo.QuantityMeasurementHistory ADD Username NVARCHAR(100) NOT NULL DEFAULT 'Anonymous';
END");

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
            PasswordHash = QuantityMeasurementApp.Authentication.PasswordHasher.Hash(seedPassword),
            Role = seedRole,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        });
    }
}

app.UseMiddleware<GlobalExceptionMiddleware>();

// Request/Response logging middleware
app.Use(async (context, next) =>
{
    var request = context.Request;
    Console.WriteLine($"\n[RequestLogger] ----");
    Console.WriteLine($"[RequestLogger] Method: {request.Method} Path: {request.Path}");
    Console.WriteLine($"[RequestLogger] Has Authorization: {request.Headers.ContainsKey("Authorization")}");
    if (request.Headers.ContainsKey("Authorization"))
    {
        var authHeader = request.Headers["Authorization"].ToString();
        Console.WriteLine($"[RequestLogger] Auth length: {authHeader.Length}, starts with: {authHeader.Substring(0, Math.Min(30, authHeader.Length))}...");
    }
    
    await next();
    
    Console.WriteLine($"[RequestLogger] Response Status: {context.Response.StatusCode}");
    Console.WriteLine($"[RequestLogger] ----\n");
});

// Enable Swagger for all environments (useful for development and testing)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();






