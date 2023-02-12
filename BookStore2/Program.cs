using System.Reflection;
using BookStore2.DbOperations;
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

app.MapControllers();

app.Run();
