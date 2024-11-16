using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.SetRoleToUser;

public class SetRoleToUserUseCaseHandler : IRequestHandler<SetRoleToUserUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public SetRoleToUserUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SetRoleToUserUseCase request, CancellationToken cancellationToken)
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

        await _unitOfWork.Roles.SetRoleToUserAsync(request.UserId, request.RoleId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}