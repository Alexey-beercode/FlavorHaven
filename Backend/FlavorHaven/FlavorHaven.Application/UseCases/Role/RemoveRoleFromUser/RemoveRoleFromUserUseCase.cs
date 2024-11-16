using MediatR;

namespace FlavorHaven.Application.UseCases.Role.RemoveRoleFromUser;

public class RemoveRoleFromUserUseCase : IRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}