using JotterService.Application.Tests.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JotterService.Api.Tests.Tools;

internal class ApplicationBuilder<TProgram, TContext> where TProgram : class where TContext : DbContext
{
    private DbOptionsFactory? _dbOptionsFactory;
    private Action<TContext>? _contextAction;

    public ApplicationBuilder<TProgram, TContext> WithSqliteDbContext() 
    {
        _dbOptionsFactory = new SqliteOptionsFactory();
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
                    if(_dbOptionsFactory is not null)
                    {
                        services.AddDbContext<TContext>(_dbOptionsFactory.GetOptionsAction());

                        using var scope = services.BuildServiceProvider().CreateScope();
                        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                        dbContext.Database.EnsureCreated();
                        if (_contextAction is not null)
                            _contextAction(dbContext);
                    }
                    
                });
            });

        return application;
    }
}
