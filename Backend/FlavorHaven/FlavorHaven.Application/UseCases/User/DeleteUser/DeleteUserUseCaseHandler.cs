using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.User.DeleteUser;

public class DeleteUserUseCaseHandler : IRequestHandler<DeleteUserUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteUserUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.Id);
        }

        await _unitOfWork.Users.DeleteAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}