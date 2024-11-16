using MediatR;

namespace FlavorHaven.Application.UseCases.User.UpdateUserBalance;

public class UpdateUserBalanceUseCase : IRequest
{
    public Guid Id { get; set; }
    public decimal Count { get; set; }
}