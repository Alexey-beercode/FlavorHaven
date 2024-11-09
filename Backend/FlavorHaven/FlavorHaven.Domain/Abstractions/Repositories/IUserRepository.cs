using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByNameAsync(string userName, CancellationToken cancellationToken=default);
    Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}