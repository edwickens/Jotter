using JotterService.Application.Interfaces;
using JotterService.Application.Configuration;
using JotterService.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JotterService.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddOptions().Configure<EncryptionOptions>(configuration.GetSection(nameof(EncryptionOptions)));
        services.AddScoped<IEncryptionService, EncryptionService>();

        return services;
    }
}