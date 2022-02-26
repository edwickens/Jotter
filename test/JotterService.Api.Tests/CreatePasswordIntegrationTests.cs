using FluentAssertions;
using JotterService.Api.Tests.Tools;
using JotterService.Application.Features;
using JotterService.Application.Tests.Tools;
using JotterService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JotterService.Api.Tests;

public class CreatePasswordIntegrationTests
{
    private readonly CancellationToken _c = CancellationToken.None;
    private readonly ApplicationBuilder<Program, ApplicationDbContext> _appBuilder = new ();
    private readonly JsonSerializerOptions _serializerOptions = new ()
    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public CreatePasswordIntegrationTests()
    {
        _appBuilder.WithSqliteDbContext();
    }

    [Fact]
    public async Task CreatePassword_ShouldCreatePassword_AndReturnCreatedPassword()
    {
        // Arrange
        var request = new CreatePassword.Request(Guid.NewGuid().ToString())
        {
            UserId = Guid.NewGuid(),
            Title = "Password",
            Url = $"https://Password.com",
            Username = "Username",
            Description = "Do-dee-do-do",
            CustomerNumber = "123",
            Secret = Guid.NewGuid().ToString()
        };

        var app = _appBuilder.Build();
        var client = app.CreateClient();

        // Act
        var response = await client.PostAsync("/Password", 
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json") );
        var result = await JsonSerializer.DeserializeAsync<CreatePassword.Response>(
            response.Content.ReadAsStream(),
            _serializerOptions);

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var createdPassword = await context.Passwords.SingleOrDefaultAsync(_c);
        // Assert
        ArgumentNullException.ThrowIfNull(createdPassword);
        ArgumentNullException.ThrowIfNull(result);
        AssertionHelper.EntityMatchesResponse(createdPassword, result).Should().BeTrue();
        AssertionHelper.EntityMatchesResponse(createdPassword, request).Should().BeTrue();
        CreatePassword.Response.Secret.Should().Be("**********");
    }



}
