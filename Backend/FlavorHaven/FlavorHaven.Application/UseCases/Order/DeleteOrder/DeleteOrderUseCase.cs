using MediatR;

namespace FlavorHaven.Application.UseCases.Order.DeleteOrder;

public class DeleteOrderUseCase : IRequest
{
    public Guid OrderId { get; set; }
}