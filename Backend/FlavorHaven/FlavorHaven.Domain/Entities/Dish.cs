using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Dish:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
}