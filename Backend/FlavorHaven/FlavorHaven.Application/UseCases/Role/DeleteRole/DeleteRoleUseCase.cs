using MediatR;

namespace FlavorHaven.Application.UseCases.Role.DeleteRole;

public class DeleteRoleUseCase : IRequest
{
    public Guid Id { get; set; }
}