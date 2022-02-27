using FluentAssertions;
using JotterService.Application.Features;
using JotterService.Application.Interfaces;
using JotterService.Application.Tests.Tools;
using JotterService.Domain;
using JotterService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JotterService.Application.Tests.Features;

public class CreatePasswordTests
{
    private readonly DbContextOptions _dbOptions = new SqliteOptionsFactory().GetOptions<ApplicationDbContext>();
    private readonly CancellationToken _c = CancellationToken.None;
    private readonly IEncryptionService _encryptionService = Substitute.For<IEncryptionService>();

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
            Secret = "jinky?77+ruh-roh"
        };

        var context = new ApplicationDbContext(_dbOptions);
        context.Database.EnsureCreated();

        _encryptionService.Encrypt(Arg.Any<string>()).Returns(new CypherText("/SECRET+/", new byte[0]));
        // Act
        var uut = new CreatePassword.Handler(context, _encryptionService);
        var result = await uut.Handle(request, _c);

        var password = await context.Passwords.SingleOrDefaultAsync(_c);
        // Assert
        ArgumentNullException.ThrowIfNull(password);
        AssertionHelper.EntityMatchesResponse(password, result).Should().BeTrue();
        AssertionHelper.EntityMatchesResponse(password, request).Should().BeTrue();
        CreatePassword.Response.Secret.Should().Be("**********");
    }



}
