using MediatR;

namespace FlavorHaven.Application.UseCases.Cart.ClearCart;

public class ClearCartUseCase : IRequest
{
    public Guid UserId { get; set; }
}