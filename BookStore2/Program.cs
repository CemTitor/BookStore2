using System.Reflection;
using BookStore2.DbOperations;
using BookStore2.Middlewares;
using BookStore2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Inject the database context into the application
builder.Services.AddDbContext<BookStoreDbContext>(options =>options.UseInMemoryDatabase(databaseName: "BookStoreDB"));

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

/// We are calling the InitializeDatabase method
app.InitializeDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/// We are using the global exception middleware
app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
