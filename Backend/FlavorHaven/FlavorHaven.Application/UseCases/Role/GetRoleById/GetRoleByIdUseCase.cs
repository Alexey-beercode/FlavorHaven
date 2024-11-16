using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.GetRoleById;

public class GetRoleByIdUseCase : IRequest<RoleDTO>
{
    public Guid Id { get; set; }
}
