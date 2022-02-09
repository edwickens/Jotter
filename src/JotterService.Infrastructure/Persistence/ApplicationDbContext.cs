using JotterService.Application.Interfaces;
using JotterService.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JotterService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Password> Passwords { get; set; } = null!;
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
