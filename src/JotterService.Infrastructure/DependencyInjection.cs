using Microsoft.Extensions.DependencyInjection;
using JotterService.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JotterService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration, string env)
    {
        if (env == "Testing")
            return services;
        if (env == "Development")
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Sqlite"),
                x => x.MigrationsAssembly("JotterService.SqliteMigrations")));
        else
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                x => x.MigrationsAssembly("JotterService.PostgresMigrations"))); 

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

}
