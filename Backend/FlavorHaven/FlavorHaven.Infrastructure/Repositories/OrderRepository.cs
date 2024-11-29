using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly AppDbContext _dbContext;
    
    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> GetByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Where(order => order.UserId == id && !order.IsDeleted)
            .Include(order => order.Status)
            .Include(order => order.OrderItems)
                .ThenInclude(item => item.Dish)
                    .ThenInclude(item=>item.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetByStatusId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Where(order => order.StatusId == id && !order.IsDeleted)
            .Include(order => order.Status)
            .Include(order => order.OrderItems)
                .ThenInclude(item => item.Dish)
                    .ThenInclude(item=>item.Category)
            .ToListAsync(cancellationToken);
    }

    public void Delete(Order order)
    {
        order.IsDeleted = true;

        _dbContext.Orders.Update(order);
    }
}