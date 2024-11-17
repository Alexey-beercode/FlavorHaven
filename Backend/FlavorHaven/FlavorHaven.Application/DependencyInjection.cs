using System.Reflection;
using FlavorHaven.Application.Models.Options;
using FlavorHaven.Application.Providers.Implementation;
using FlavorHaven.Application.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FlavorHaven.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection(TokenOptions.DefaultSection));
        
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IPasswordProvider, PasswordProvider>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        return services;
    }
}