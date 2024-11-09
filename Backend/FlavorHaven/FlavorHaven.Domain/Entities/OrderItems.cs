using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class OrderItems : BaseEntity
{
    public int Count { get; set; }
    
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
}