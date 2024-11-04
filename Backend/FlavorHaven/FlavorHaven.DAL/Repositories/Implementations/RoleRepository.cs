using FlavorHaven.DAL.Configuration;
using FlavorHaven.DAL.Repositories.Interfaces;
using FlavorHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.DAL.Repositories.Implementations;

public class RoleRepository:BaseRepository<Role>,IRoleRepository
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
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);

        if (user == null || role == null)
        {
            return false;
        }

        var isExists =
            await _dbContext.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
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
        return await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted);
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
        _dbSet.Update(role);
        
        await _dbContext.UserRoles
            .Where(userRole => userRole.RoleId == role.Id && !userRole.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(userRole => userRole.IsDeleted, true), cancellationToken);
    }
}