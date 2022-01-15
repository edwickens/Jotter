using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace JotterService.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetAllPasswords_ReturnsPasswords()
        {
            // Arrange
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/Password");
            var result = await JsonSerializer.DeserializeAsync<IEnumerable<Password>>(response.Content.ReadAsStream());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ArgumentNullException.ThrowIfNull(result);
            result.Select(r => r.Secret).Should().AllBe("**********");

        }
    }
}