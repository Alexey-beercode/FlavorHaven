using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.User.GetAllUsers;

public class GetAllUsersUseCase : IRequest<IEnumerable<UserDTO>>
{
}