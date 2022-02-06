using FluentAssertions;
using JotterService.Application.Features;
using JotterService.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using JotterService.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using JotterService.Application;

namespace JotterService.Api.Tests;

public class GetAllPasswordsIntegrationTest
{
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions() 
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

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
            var c = context.Database.EnsureCreated();
            context.Passwords.AddRange(passwords!);
            context.SaveChanges();
        }

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                          options.UseSqlite("DataSource=file::memory:?cache=shared"));
                    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
                    using (var scope = services.BuildServiceProvider().CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        Seed(dbContext);
                    }
                }
                );
            });

        var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/Password");
        var resultString = await response.Content.ReadAsStringAsync();
        var result = await JsonSerializer.DeserializeAsync<IEnumerable<GetPasswords.Response>>(
            response.Content.ReadAsStream(),
            _serializerOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(result);
        result.Zip(passwords.OrderBy(p=> p.Title), (r, e) => Tuple.Create(r, e))
            .All(p=> EntityMatchesResponse(p.Item2, p.Item1)).Should().BeTrue();
        result.Select(r => r.Secret).Should().AllBe("**********");
    }

    private bool EntityMatchesResponse(Password entity, GetPasswords.Response response)
    {
        if (entity is null)
            return false;
        if (response is null)
            return false;

        if (!entity.Id.Equals(response.Id))
            return false;
        if (!entity.UserId.Equals(response.UserId))
            return false;
        if (entity.Url != response.Url)
            return false;
        if (entity.Title != response.Title)
            return false;
        if (entity.Description != response.Description)
            return false;
        if (entity.Username != response.Username)
            return false;
        if (entity.CustomerNumber != response.CustomerNumber)
            return false;
        

        return true;
    }
}