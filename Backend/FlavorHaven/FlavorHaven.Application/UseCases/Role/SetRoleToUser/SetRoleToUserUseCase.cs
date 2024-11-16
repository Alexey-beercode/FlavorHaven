using MediatR;

namespace FlavorHaven.Application.UseCases.Role.SetRoleToUser;

public class SetRoleToUserUseCase : IRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}