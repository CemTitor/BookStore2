using System.Reflection;
using System.Text;
using BookStore2.DbOperations;
using BookStore2.Middlewares;
using BookStore2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, // Who can use the token
            ValidateIssuer = false, // Who issued the token
            ValidateLifetime = true, // Is the token expired
            ValidateIssuerSigningKey = true, // Key that is used to sign the token
            ValidIssuer = Configuration["Token:Issuer"],
            ValidAudience = Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });
// Inject the database context into the application
builder.Services.AddDbContext<BookStoreDbContext>(options =>options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
/// We are adding the dependency injection for the BookStoreDbContext
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Automapper is a library that is used to map objects to each other.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Add the dependency injection for the logger service 
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
var app = builder.Build();
/// We are calling the InitializeDatabase extension method
app.InitializeDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

/// We are using the global exception middleware
app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
