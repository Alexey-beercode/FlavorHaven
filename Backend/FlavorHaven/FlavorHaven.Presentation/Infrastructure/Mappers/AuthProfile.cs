using AutoMapper;
using FlavorHaven.Application.UseCases.Auth.Login;
using FlavorHaven.Application.UseCases.Auth.Register;
using FlavorHaven.DTOs.Auth;

namespace FlavorHaven.Infrastructure.Mappers;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<LoginRequestDTO, LoginUseCase>();
        CreateMap<RegisterRequestDTO, RegisterUseCase>();
    }
}