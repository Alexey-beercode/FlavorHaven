using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Review : BaseEntity
{
    public string Note { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}