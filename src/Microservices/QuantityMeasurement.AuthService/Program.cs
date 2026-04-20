using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuantityMeasurement.AuthService.Data;
using QuantityMeasurement.SharedKernel.Auth;
using QuantityMeasurement.SharedKernel.Repository;

var builder = WebApplication.CreateBuilder(args);

// ── JWT ──────────────────────────────────────────────────────────────────────
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);
var jwtOptions = jwtSection.Get<JwtOptions>()!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = jwtOptions.Issuer,
        ValidAudience            = jwtOptions.Audience,
        IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
        ClockSkew                = TimeSpan.Zero
    });
builder.Services.AddAuthorization();

// ── Database ─────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AuthDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("QuantityMeasurementDb")));

// ── DI ───────────────────────────────────────────────────────────────────────
builder.Services.AddScoped<IUserCredentialRepository, UserCredentialEfRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ── MVC + Swagger ────────────────────────────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth Service", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", Type = SecuritySchemeType.Http,
        Scheme = "bearer", BearerFormat = "JWT", In = ParameterLocation.Header
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
        Array.Empty<string>()
    }});
});

builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// ── DB init (non-fatal — service starts even if DB is temporarily unreachable) ──
try
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    db.Database.ExecuteSqlRaw(@"
IF OBJECT_ID('dbo.UserCredentials', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.UserCredentials (
        Id            INT IDENTITY(1,1) PRIMARY KEY,
        Username      NVARCHAR(100)  NOT NULL UNIQUE,
        PasswordHash  NVARCHAR(256)  NOT NULL,
        Role          NVARCHAR(50)   NOT NULL,
        IsActive      BIT            NOT NULL CONSTRAINT DF_UserCredentials_IsActive DEFAULT 1,
        CreatedAtUtc  DATETIME2      NOT NULL CONSTRAINT DF_UserCredentials_CreatedAtUtc DEFAULT SYSUTCDATETIME()
    );
END");

    var cfg      = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var repo     = scope.ServiceProvider.GetRequiredService<IUserCredentialRepository>();
    var seedUser = cfg["SeedUser:Username"];
    var seedPass = cfg["SeedUser:Password"];
    var seedRole = cfg["SeedUser:Role"] ?? "User";

    if (!string.IsNullOrWhiteSpace(seedUser) && !string.IsNullOrWhiteSpace(seedPass) && !repo.Exists(seedUser))
    {
        repo.Add(new QuantityMeasurement.SharedKernel.Repository.UserCredentialRecord
        {
            Username     = seedUser,
            PasswordHash = QuantityMeasurement.SharedKernel.Auth.PasswordHasher.Hash(seedPass),
            Role         = seedRole,
            IsActive     = true,
            CreatedAtUtc = DateTime.UtcNow
        });
        logger.LogInformation("Seed user '{User}' created successfully.", seedUser);
    }
}
catch (Exception ex)
{
    // Log the DB error but DO NOT crash — the service will still start.
    // Fix: add your current IP to the Azure SQL Server firewall rules.
    var startupLogger = app.Services.GetRequiredService<ILogger<Program>>();
    startupLogger.LogWarning(
        "⚠️  DB initialization skipped — could not connect to SQL Server at startup: {Message}. " +
        "Service is running but DB operations will fail until connectivity is restored. " +
        "If using Azure SQL, add your IP to the firewall in Azure Portal.",
        ex.Message);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
