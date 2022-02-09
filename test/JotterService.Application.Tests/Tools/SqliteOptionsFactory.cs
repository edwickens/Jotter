using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace JotterService.Application.Tests.Tools;

public class SqliteOptionsFactory : DbOptionsFactory
{
    public override DbContextOptions GetOptions<TContext>()
    {
        var connection = new SqliteConnection($"Data Source=file:{Guid.NewGuid()}?mode=memory");
        connection.Open();
        return new DbContextOptionsBuilder<TContext>()
            .UseSqlite(connection)
            .Options;
    }
}
