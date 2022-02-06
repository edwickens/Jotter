using FluentAssertions;
using JotterService.Application.Features;
using JotterService.Domain;
using JotterService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JotterService.Application.Tests;

public class GetAllPasswordsTests
{
    private readonly DbContextOptions _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=file::memory:")
            .Options;

    private readonly CancellationToken _c = CancellationToken.None;

    [Fact]
    public async Task GetAllPasswords_ShouldReturnPasswordsAsync()
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

        var context = new ApplicationDbContext(_dbOptions);
        context.Database.EnsureCreated();
        await context.Passwords.AddRangeAsync(passwords, _c);
        await context.SaveChangesAsync(_c);
        // Act
        var uut = new GetAllPasswords.Handler(context);
        var result = await uut.Handle(new GetAllPasswords.Request() { }, _c);

        // Assert 
        result.Count().Should().Be(passwords.Count());
        result.Zip(passwords.OrderBy(p => p.Title), (r, e) => Tuple.Create(r, e))
            .All(p => AssertionHelper.EntityMatchesResponse(p.Item2, p.Item1)).Should().BeTrue();
        result.Select(r => r.Secret).Should().AllBe("**********");
    }

    [Fact]
    public async Task GetAllPasswords_WhenNoPasswords_ShouldReturnEmptyList()
    {
        // Arrange
        var context = new ApplicationDbContext(_dbOptions);
        context.Database.EnsureCreated();
        // Act
        var uut = new GetAllPasswords.Handler(context);
        var result = await uut.Handle(new GetAllPasswords.Request() { }, _c);

        // Assert 
        result.Count().Should().Be(0);
    }
}