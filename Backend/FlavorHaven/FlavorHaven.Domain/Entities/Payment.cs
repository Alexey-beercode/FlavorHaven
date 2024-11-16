using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public bool IsCanceled { get; set; }
    
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
}