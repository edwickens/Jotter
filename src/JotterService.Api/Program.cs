var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options.WithOrigins("http://localhost:4200"));

app.MapGet("/Password", () =>
{
    return Enumerable.Range(1, 5).Select(index =>
       new Password
       (
           Guid.NewGuid(),
           Guid.NewGuid(),
           "Password"+index.ToString(),
           $"https://{"Password" + index.ToString()}.com",
           "Username" + index.ToString(),
           "",
           ""
       ))
        .ToArray();
})
.WithName("GetPasswords");

app.Run();

// To make implict program class public for integration tests
public partial class Program { }

public record Password(Guid Id, Guid UserId, string Title, string Url,
    string Username, string Description, string CustomerNumber){
    public string Secret => "**********";
}