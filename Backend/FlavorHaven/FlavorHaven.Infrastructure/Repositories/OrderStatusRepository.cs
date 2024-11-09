using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class OrderStatusRepository : BaseRepository<OrderStatus>, IOrderStatusRepository
{
    private readonly AppDbContext _dbContext;
    
    public OrderStatusRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderStatus> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.OrderStatuses
            .FirstOrDefaultAsync(status => status.Name == name && !status.IsDeleted, cancellationToken);
    }

    public void Delete(OrderStatus status)
    {
        status.IsDeleted = true;

        _dbContext.OrderStatuses.Update(status);
    }
}