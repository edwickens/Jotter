using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JotterService.Application.Interfaces;
using JotterService.Infrastructure.Persistence;

namespace JotterService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration, string env)
    {
        if (env == "Development")
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Sqlite"),
                x => x.MigrationsAssembly("JotterService.SqliteMigrations")));
        else if(env != "Testing")
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                x => x.MigrationsAssembly("JotterService.PostgresMigrations"))); 

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

}
