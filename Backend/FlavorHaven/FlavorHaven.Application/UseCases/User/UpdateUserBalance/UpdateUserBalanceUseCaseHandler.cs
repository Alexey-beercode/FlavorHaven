using FlavorHaven.Application.Exceptions;
using FlavorHaven.Domain.Abstractions.UnitOfWork;
using MediatR;

namespace FlavorHaven.Application.UseCases.User.UpdateUserBalance;

public class UpdateUserBalanceUseCaseHandler : IRequestHandler<UpdateUserBalanceUseCase>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserBalanceUseCaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserBalanceUseCase request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.User), request.Id);
        }

        user.Balance += request.Count;
        
        if (user.Balance < 0)
        {
            throw new InvalidOperationException("Balance cannot be negative");
        }
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}