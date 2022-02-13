using FluentAssertions;
using JotterService.Application.Features;
using JotterService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using JotterService.Application.Tests.Tools;
using JotterService.Infrastructure.Persistence;
using JotterService.Api.Tests.Tools;

namespace JotterService.Api.Tests;

public class GetAllPasswordsIntegrationTests
{
    private readonly ApplicationBuilder<Program, ApplicationDbContext> _appBuilder = new();
    private readonly JsonSerializerOptions _serializerOptions = new () 
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public GetAllPasswordsIntegrationTests()
    {
        _appBuilder.WithSqliteDbContext();
    }

    [Fact]
    public async Task GetAllPasswords_ReturnsPasswords()
    {
        // Arrange
        var passwords = Enumerable.Range(1, 5).Select(index =>
         new Password()
         {
             Id = Guid.NewGuid(),
             UserId = Guid.NewGuid(),
             Title = "Password" + index.ToString(),
             Url = $"https://{"Password" + index.ToString()}.com",
             Username = "Username" + index.ToString(),
             Description = "",
             CustomerNumber = "",
             Secret = Guid.NewGuid().ToString()
         }
        )
        .ToArray();

        void Seed(ApplicationDbContext context)
        {
            context.Passwords.AddRange(passwords!);
            context.SaveChanges();
        }

        _appBuilder.WithDbContextAction(Seed);
        
        var application = _appBuilder.Build();

        var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/Password");
        var resultString = await response.Content.ReadAsStringAsync();
        var result = await JsonSerializer.DeserializeAsync<IEnumerable<GetAllPasswords.Response>>(
            response.Content.ReadAsStream(),
            _serializerOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(result);
        result.Zip(passwords.OrderBy(p=> p.Title), (r, e) => Tuple.Create(r, e))
            .All(p=> AssertionHelper.EntityMatchesResponse(p.Item2, p.Item1)).Should().BeTrue();
        result.Select(r => r.Secret).Should().AllBe("**********");
    }
}