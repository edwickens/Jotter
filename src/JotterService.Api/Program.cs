var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

internal record Password(Guid Id, Guid UserId, string Title, string Url,
    string Username, string Description, string CustomerNumber){
    public string Secret => "**********";
}