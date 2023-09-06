using powerplant_coding_challenge.Models;
using powerplant_coding_challenge.Services;
using Serilog;
using Serilog.Events;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Configuration de Serilog pour les logs d'information et les logs d'erreur
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("logs/information.log", rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/error.log", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Services.AddScoped<ServicePowerPlantData>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
