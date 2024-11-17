using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Role.CreateRole;
using FlavorHaven.Application.UseCases.Role.UpdateRole;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Infrastructure.Mappers;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDTO>();
        CreateMap<CreateRoleUseCase, Role>();
        CreateMap<UpdateRoleUseCase, Role>();
    }
}