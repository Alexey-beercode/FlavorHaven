using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.UpdateOrderStatus;

public class UpdateOrderStatusUseCase : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}