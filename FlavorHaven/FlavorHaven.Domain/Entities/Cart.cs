using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Cart:BaseEntity
{
    public Guid UserId { get; set; }
    public Guid DishId { get; set; }
    public int Count { get; set; }
}