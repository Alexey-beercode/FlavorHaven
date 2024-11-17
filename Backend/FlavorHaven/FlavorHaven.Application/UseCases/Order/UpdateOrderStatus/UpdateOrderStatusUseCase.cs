using MediatR;

namespace FlavorHaven.Application.UseCases.Order.UpdateOrderStatus;

public class UpdateOrderStatusUseCase : IRequest
{
    public Guid OrderId { get; set; }
    public Guid StatusId { get; set; }
}