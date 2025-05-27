using Microsoft.AspNetCore.Authentication.Cookies;

using DartsApp.Server.Services;
using DartsApp.Server.Middlewares;
using DartsApp.Server.Facades.AuthenticationService;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/AuthenticationService/Login";
    });
builder.Services.AddAuthorization();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// add EntityFrameworkCore
builder.Services.AddDbContext<DartsDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DartsDbConnectionString")));

// OpenApi configuration
builder.Services.AddControllers(options =>
{
    // ќтключаем text/plain и другие форматы
    options.OutputFormatters.RemoveType<StringOutputFormatter>();

    // явно указываем, что API использует только JSON
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
});

// add own Services
builder.Services.AddScoped<IGameServiceFacade, GameServiceFacade>();
builder.Services.AddScoped<IUserServiceFacade, UserServiceFacade>();
builder.Services.AddScoped<IAuthenticationServiceFacade, AuthenticationServiceFacade>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Enable Authentication
app.UseAuthentication();
app.UseAuthorization();

// Enable CORS
app.UseCors("AllowAllOrigins");

// Use the request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();

public partial class Program { }
