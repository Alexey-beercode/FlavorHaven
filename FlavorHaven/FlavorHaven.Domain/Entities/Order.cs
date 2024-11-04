using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Order:BaseEntity
{
    public string OrderNumber { get; set; }
    public DateTime CreatedTime { get; set; }
    public Guid UserId { get; set; }
    public Guid StatusId { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public string Address { get; set; }
    public bool IsPayed { get; set; }
}