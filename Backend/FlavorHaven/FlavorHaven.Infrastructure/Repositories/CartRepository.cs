using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    private readonly AppDbContext _dbContext;
    
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Cart>> GetByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Cart
            .Where(c => c.UserId == id && !c.IsDeleted)
            .Include(c => c.Dish)
                .ThenInclude(item=>item.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var itemsToDelete = await _dbContext.Cart.Where(c => c.UserId == id)
            .ToListAsync(cancellationToken);
        
        _dbContext.Cart.RemoveRange(itemsToDelete);
    }

    public void Delete(Cart cart)
    {
        _dbContext.Cart.Remove(cart);
    }
}