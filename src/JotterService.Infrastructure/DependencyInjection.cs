using Microsoft.Extensions.DependencyInjection;
using JotterService.Application;
using Microsoft.EntityFrameworkCore;

namespace JotterService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isDevelopment = false)
    {
        if (isDevelopment)
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Filename=jotterDb.sqlite", x => x.MigrationsAssembly("JotterService.SqliteMigrations")));
        else
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("")); 

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }

}
