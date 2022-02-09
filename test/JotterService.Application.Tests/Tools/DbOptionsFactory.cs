using Microsoft.EntityFrameworkCore;

namespace JotterService.Application.Tests.Tools;

public abstract class DbOptionsFactory
{
    public abstract DbContextOptions GetOptions<TContext>() where TContext : DbContext;
}
