using JotterService.Application.Tests.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JotterService.Api.Tests.Tools;

internal class ApplicationBuilder<TProgram, TContext> where TProgram : class where TContext : DbContext
{
    private DbContextOptions? _dbOptions;
    private Action<TContext>? _contextAction;

    //TODO: get dbContext type from WithSqliteDbContext method
    public ApplicationBuilder<TProgram, TContext> WithSqliteDbContext() 
    {
        _dbOptions = new SqliteOptionsFactory().GetOptions<TContext>();
        return this;
    }

    public ApplicationBuilder<TProgram, TContext> WithDbContextAction(Action<TContext> action)
    {
        _contextAction = action;
        return this;
    }

    public WebApplicationFactory<TProgram> Build()
    {
        var application = new WebApplicationFactory<TProgram>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    if(_dbOptions is not null)
                    {
                        var connection = new SqliteConnection($"Data Source=file:{Guid.NewGuid()}?mode=memory");
                        //TODO: make connection string, db name configurable; make use of db options
                        services.AddDbContext<TContext>(options =>
                        {
                            connection.Open();
                            options.UseSqlite(connection);
                        });
                       
                        using (var scope = services.BuildServiceProvider().CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                            //dbContext.Database.EnsureDeleted();
                            dbContext.Database.EnsureCreated();
                            if (_contextAction is not null)
                                _contextAction(dbContext);
                        }
                    }
                    
                });
            });

        return application;
    }
}
