using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.GetAllRoles;

public class GetAllRolesUseCase : IRequest<IEnumerable<RoleDTO>>
{
}