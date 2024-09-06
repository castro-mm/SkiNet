using Api.Controllers.Extensions;
using Api.Middleware;
using Api.SignalR;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;

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

// Identity
builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<StoreContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerServices();
builder.Services.AddSignalR();

//builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.AddSwaggerAppServices();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();


//Identity
app.MapGroup("api").MapIdentityApi<AppUser>(); //api/login

// SignalR Hub Map
app.MapHub<NotificationHub>("hub/notifications");
app.MapFallbackToController("Index", "Fallback");
// Seed data
await app.SeedData();

app.Run();
