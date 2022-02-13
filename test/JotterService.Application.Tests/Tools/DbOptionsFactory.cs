using Microsoft.EntityFrameworkCore;
using System;

namespace JotterService.Application.Tests.Tools;

public abstract class DbOptionsFactory
{
    public abstract DbContextOptions GetOptions<TContext>() where TContext : DbContext;
    public abstract Action<DbContextOptionsBuilder> GetOptionsAction();
}
