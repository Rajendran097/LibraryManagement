
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
        builder.WithOrigins("http://localhost:5173") 
               .AllowAnyMethod()
               .AllowAnyHeader() 
               .AllowCredentials()); 
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<libraryContext>(options =>
    options.UseSqlServer(connectionString, opts => opts.EnableRetryOnFailure())
           .LogTo(Console.WriteLine, LogLevel.Information));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); 
app.UseAuthorization(); 

app.MapControllers();


app.Run();