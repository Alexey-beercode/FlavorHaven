using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<IEnumerable<Review>> GetByUserId(Guid id, CancellationToken cancellationToken = default);
    Task<Review> GetByOrderId(Guid id, CancellationToken cancellationToken = default);
    void Delete(Review review);
}