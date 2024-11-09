using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByNameAsync(string userName, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
    }
    
    public async Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => 
                    u.RefreshToken == refreshToken && 
                    !u.IsDeleted && 
                    u.RefreshTokenExpiryTime > DateTime.UtcNow.ToUniversalTime(),
                cancellationToken);
    }
    
    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        user.IsDeleted = true;
        
        _dbContext.Users.Update(user);
        
        await _dbContext.UserRoles
            .Where(userRole => userRole.UserId == user.Id && !userRole.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(userRole => userRole.IsDeleted, true), cancellationToken);
    }
}