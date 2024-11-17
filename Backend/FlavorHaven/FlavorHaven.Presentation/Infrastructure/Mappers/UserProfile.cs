using AutoMapper;
using FlavorHaven.Application.UseCases.User.UpdateUserBalance;
using FlavorHaven.DTOs.User;

namespace FlavorHaven.Infrastructure.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateUserBalanceRequestDTO, UpdateUserBalanceUseCase>();
    }
}