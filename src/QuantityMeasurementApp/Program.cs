using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Middleware;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Interfaces;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Services;
using QuantityMeasurementApp.QuantityMeasurementRepo;
using QuantityMeasurementApp.QuantityMeasurementRepo.Implementations;
using QuantityMeasurementApp.QuantityMeasurementRepo.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuantityMeasurementDb")));

builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
