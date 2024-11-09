using FlavorHaven.Domain.Abstractions.Repositories;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Infrastructure.Configuration;

namespace FlavorHaven.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly ICartRepository _cartRepository;
    private readonly IDishCategoryRepository _dishCategoryRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private bool _disposed;

    public UnitOfWork(
        AppDbContext dbContext,
        ICartRepository cartRepository,
        IDishCategoryRepository dishCategoryRepository,
        IDishRepository dishRepository,
        IOrderRepository orderRepository,
        IOrderStatusRepository orderStatusRepository,
        IPaymentRepository paymentRepository,
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _dbContext = dbContext;
        _cartRepository = cartRepository;
        _dishCategoryRepository = dishCategoryRepository;
        _dishRepository = dishRepository;
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
        _paymentRepository = paymentRepository;
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public ICartRepository Cart => _cartRepository;
    public IDishCategoryRepository DishCategories => _dishCategoryRepository;
    public IDishRepository Dishes => _dishRepository;
    public IOrderRepository Orders => _orderRepository;
    public IOrderStatusRepository OrderStatuses => _orderStatusRepository;
    public IPaymentRepository Payments => _paymentRepository;
    public IReviewRepository Reviews => _reviewRepository;
    public IUserRepository Users => _userRepository;
    public IRoleRepository Roles => _roleRepository;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}