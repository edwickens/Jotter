using JotterService.Application;
using JotterService.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o=> 
    o.CustomSchemaIds(t => t.DeclaringType?.Name + "." + t.Name)
);
builder.Services.AddCors();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors(options => options.WithOrigins("http://localhost:4200"));

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    using var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    ctx.Database.Migrate();
}

app.Run();

// To make implict program class public for integration tests
public partial class Program { }

