using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration.Database;
using FlavorHaven.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Configuration;

public class AppDbContext : DbContext
{
    public DbSet<Cart> Cart { get; set; }
    public DbSet<Dish> Dishes { get; set;}
    public DbSet<DishCategory> DishCategories { get; set;}
    public DbSet<Order> Orders { get; set;}
    public DbSet<OrderItems> OrderItems { get; set;}
    public DbSet<OrderStatus> OrderStatuses { get; set;}
    public DbSet<Payment> Payments { get; set;}
    public DbSet<Review> Reviews { get; set;}
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.SeedUsersRolesData();
        modelBuilder.SeedOrderStatuses();
    }
}