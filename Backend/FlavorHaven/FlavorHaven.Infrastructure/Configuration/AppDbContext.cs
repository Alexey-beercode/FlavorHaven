using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration.Database;
using FlavorHaven.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.SeedUsersRolesData();
    }
}