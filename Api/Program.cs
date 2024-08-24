using Api.Controllers.Extensions;
using Api.Middleware;
using Core.Interfaces;
using Infrastructure.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Define the services related to database
builder.Services.AddDatabaseServices(builder.Configuration);

// Define the association between the classes and interfaces as Scoped, Transient or Singleton
builder.Services.AddMappingClassesServices();

builder.Services.AddCors();

// Redis
builder.Services.AddRedisServicesExtensions(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.AddSwaggerAppServices();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.MapControllers();

// Seed data
await app.SeedData();

app.Run();
