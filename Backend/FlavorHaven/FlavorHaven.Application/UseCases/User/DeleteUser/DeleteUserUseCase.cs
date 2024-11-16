using MediatR;

namespace FlavorHaven.Application.UseCases.User.DeleteUser;

public class DeleteUserUseCase : IRequest
{
    public Guid Id { get; set; }
}