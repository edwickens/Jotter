using FluentAssertions;
using JotterService.Application.Features;
using JotterService.Application.Tests.Tools;
using JotterService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JotterService.Application.Tests;

public class CreatePasswordTests
{
    private readonly DbContextOptions _dbOptions = new SqliteOptionsFactory().GetOptions<ApplicationDbContext>();
    private readonly CancellationToken _c = CancellationToken.None;

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
            CustomerNumber = "123"
        };

        var context = new ApplicationDbContext(_dbOptions);
        context.Database.EnsureCreated();
        // Act
        var uut = new CreatePassword.Handler(context);
        var result = await uut.Handle(request, _c);

        var password = await context.Passwords.SingleOrDefaultAsync(_c);
        // Assert
        ArgumentNullException.ThrowIfNull(password);
        AssertionHelper.EntityMatchesResponse(password, result).Should().BeTrue();
        AssertionHelper.EntityMatchesResponse(password, request).Should().BeTrue();
        CreatePassword.Response.Secret.Should().Be("**********");
    }



}
