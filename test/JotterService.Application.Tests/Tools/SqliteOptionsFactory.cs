using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace JotterService.Application.Tests.Tools;

public class SqliteOptionsFactory : DbOptionsFactory
{
    private readonly SqliteConnection connection = new ($"Data Source=file:{Guid.NewGuid()}?mode=memory");
    public override DbContextOptions GetOptions<TContext>()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        GetOptionsAction().Invoke(optionsBuilder);
        return optionsBuilder.Options;
    }

    public override Action<DbContextOptionsBuilder> GetOptionsAction()
    {
        return new Action<DbContextOptionsBuilder>(options =>
        {
            connection.Open();
            options.UseSqlite(connection);
        });        
    }
}
