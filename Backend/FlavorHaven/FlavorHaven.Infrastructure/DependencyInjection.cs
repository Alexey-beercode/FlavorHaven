using System.Reflection;
using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Infrastructure.Configuration;
using FlavorHaven.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlavorHaven.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurePostgreSql(services, configuration);
        
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IDishCategoryRepository, DishCategoryRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        
        return services;
    }

    private static void ConfigurePostgreSql(IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        string? dataBaseConnection = configuration.GetConnectionString("PostrgeSql");
        services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(dataBaseConnection));
    }
}