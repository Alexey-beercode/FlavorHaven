using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using FlavorHaven.Domain.Entities;
using MediatR;

namespace FlavorHaven.Application.UseCases.Auth.Revoke;

public class RevokeUseCaseHandler : IRequestHandler<RevokeUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public RevokeUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RevokeUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(User), request.UserId);
        }
        
        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = DateTime.MinValue;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}