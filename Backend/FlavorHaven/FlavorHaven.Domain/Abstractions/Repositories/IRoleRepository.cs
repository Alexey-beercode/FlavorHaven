using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Domain.Abstractions.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<IEnumerable<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
    Task<bool> SetRoleToUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
    Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
}