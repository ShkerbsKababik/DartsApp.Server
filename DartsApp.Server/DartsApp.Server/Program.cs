using DartsApp.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

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
        options.LoginPath = "AuthenticationService/Login";
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

// add own Services
builder.Services.AddScoped<IGameServiceFacade, GameServiceFacade>();
builder.Services.AddScoped<IUserServiceFacade, UserServiceFacade>();
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

app.Run();

public partial class Program { }
