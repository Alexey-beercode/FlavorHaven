using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    private readonly AppDbContext _dbContext;
    
    public ReviewRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Review>> GetByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Reviews
            .Where(review => review.UserId == id && !review.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Review> GetByOrderId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Reviews
            .Where(review => review.OrderId == id && !review.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Delete(Review review)
    {
        review.IsDeleted = true;

        _dbContext.Reviews.Update(review);
    }
}
