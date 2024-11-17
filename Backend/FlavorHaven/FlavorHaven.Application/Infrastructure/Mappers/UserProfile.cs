using AutoMapper;
using FlavorHaven.Application.Models.DTOs;
using FlavorHaven.Application.UseCases.Auth.Register;
using FlavorHaven.Domain.Entities;

namespace FlavorHaven.Application.Infrastructure.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<RegisterUseCase, User>();
    }
}