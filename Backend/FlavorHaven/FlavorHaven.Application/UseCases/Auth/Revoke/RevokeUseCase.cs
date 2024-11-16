using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.Revoke;

public class RevokeUseCase : IRequest
{
    public Guid UserId { get; set; }
}