using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.RemoveRoleFromUser;

public class RemoveRoleFromUserUseCaseHandler : IRequestHandler<RemoveRoleFromUserUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRoleFromUserUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveRoleFromUserUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.UserId);
        }

        var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Role), request.RoleId);
        }
        
        await _unitOfWork.Roles.RemoveRoleFromUserAsync(request.UserId, request.RoleId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}