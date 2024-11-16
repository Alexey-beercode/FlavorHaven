using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Entities;
using FlavorHaven.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlavorHaven.Infrastructure.Repositories;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    private readonly AppDbContext _dbContext;
    
    public PaymentRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Payment>> GetByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Payments
            .Where(payment => payment.UserId == id && !payment.IsDeleted)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Payment> GetByOrderId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Payments
            .FirstOrDefaultAsync(payment => payment.OrderId == id && !payment.IsDeleted, cancellationToken);
    }

    public void Delete(Payment payment)
    {
        payment.IsCanceled = true;

        _dbContext.Payments.Update(payment);
    }
}
