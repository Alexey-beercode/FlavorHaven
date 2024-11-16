using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.Role.DeleteRole;

public class DeleteRoleUseCaseHandler : IRequestHandler<DeleteRoleUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteRoleUseCase request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(request.Id, cancellationToken);
        if (role is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Role), request.Id);
        }

        await _unitOfWork.Roles.DeleteAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}