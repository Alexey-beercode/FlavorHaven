using FlavorHaven.Application.Models.DTOs;
using MediatR;

namespace FlavorHaven.Application.UseCases.User.GetUserById;

public class GetUserByIdUseCase : IRequest<UserDTO>
{
    public Guid Id { get; set; }
}
