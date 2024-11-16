using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.GetRoleByName;

public class GetRoleByNameUseCase : IRequest<RoleDTO>
{
    public string Name { get; set; }
}