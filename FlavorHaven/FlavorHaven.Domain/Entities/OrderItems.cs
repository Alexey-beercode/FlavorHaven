using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class OrderItems:BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid DishId { get; set; }
    public int Count { get; set; }
}