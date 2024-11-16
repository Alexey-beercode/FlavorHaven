using MediatR;

namespace FlavorHaven.Application.UseCases.Role.CreateRole;

public class CreateRoleUseCase : IRequest
{
    public string Name { get; set; }
}