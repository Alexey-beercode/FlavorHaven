using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.GetRolesByUserId;

public class GetRolesByUserIdUseCase : IRequest<IEnumerable<RoleDTO>>
{
    public Guid UserId { get; set; }
}
