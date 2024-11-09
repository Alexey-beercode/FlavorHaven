using FlavorHaven.Domain.Abstractions.Repositories;

namespace FlavorHaven.Domain.Abstractions.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    ICartRepository Cart { get; }
    IDishCategoryRepository DishCategories { get; }
    IDishRepository Dishes { get; }
    IOrderRepository Orders { get; }
    IOrderStatusRepository OrderStatuses { get; }
    IPaymentRepository Payments { get; }
    IReviewRepository Reviews { get; }
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}