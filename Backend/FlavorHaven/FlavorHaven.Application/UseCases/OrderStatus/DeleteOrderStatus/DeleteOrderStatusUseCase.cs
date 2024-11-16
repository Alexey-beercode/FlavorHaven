using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.DeleteOrderStatus;

public class DeleteOrderStatusUseCase : IRequest
{
    public Guid Id { get; set; }
}