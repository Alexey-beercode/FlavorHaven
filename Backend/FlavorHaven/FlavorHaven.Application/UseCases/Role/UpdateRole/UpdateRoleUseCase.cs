using MediatR;

namespace FlavorHaven.Application.UseCases.Role.UpdateRole;

public class UpdateRoleUseCase : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}