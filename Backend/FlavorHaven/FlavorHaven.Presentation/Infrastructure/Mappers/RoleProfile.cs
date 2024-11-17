using AutoMapper;
using FlavorHaven.Application.UseCases.Role.CreateRole;
using FlavorHaven.Application.UseCases.Role.RemoveRoleFromUser;
using FlavorHaven.Application.UseCases.Role.SetRoleToUser;
using FlavorHaven.Application.UseCases.Role.UpdateRole;
using FlavorHaven.DTOs.Role;

namespace FlavorHaven.Infrastructure.Mappers;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleRequestDTO, CreateRoleUseCase>();
        CreateMap<RoleRequestDTO, UpdateRoleUseCase>();
        CreateMap<UserRoleRequestDTO, SetRoleToUserUseCase>();
        CreateMap<UserRoleRequestDTO, RemoveRoleFromUserUseCase>();
    }
}