using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly AppDbContext _dbContext;
    
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken=default)
    {
        return await _dbContext.Roles.AsNoTracking()
            .Where(role => _dbContext.UserRoles
                .Any(userRole => userRole.UserId == userId && !userRole.IsDeleted && role.Id==userRole.RoleId))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> SetRoleToUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var isExists = await _dbContext.UserRoles
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
        if (isExists)
        {
            return false;
        }

        var userRole = new UserRole { UserId = userId, RoleId = roleId };
        
        await _dbContext.UserRoles.AddAsync(userRole, cancellationToken);
        
        return true;
    }

    public async Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted, cancellationToken);
    }

    public async Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var userRole = await _dbContext.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

        if (userRole == null)
        {
            return false;
        }

        userRole.IsDeleted = true;
        _dbContext.UserRoles.Update(userRole);
        return true;
    }
    
    public async Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        role.IsDeleted = true;
        
        _dbContext.Roles.Update(role);
        
        await _dbContext.UserRoles
            .Where(userRole => userRole.RoleId == role.Id && !userRole.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(userRole => userRole.IsDeleted, true), cancellationToken);
    }
}