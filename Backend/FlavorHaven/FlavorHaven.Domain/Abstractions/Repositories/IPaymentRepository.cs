using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByUserId(Guid id, CancellationToken cancellationToken = default);
    void Delete(Payment payment);
}