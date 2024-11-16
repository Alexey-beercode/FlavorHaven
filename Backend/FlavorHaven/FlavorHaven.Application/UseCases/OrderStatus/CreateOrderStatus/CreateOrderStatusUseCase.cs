using MediatR;

namespace FlavorHaven.Application.UseCases.OrderStatus.CreateOrderStatus;

public class CreateOrderStatusUseCase : IRequest
{
    public string Name { get; set; }
}