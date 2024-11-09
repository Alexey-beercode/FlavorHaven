using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Cart : BaseEntity
{
    public int Count { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
}