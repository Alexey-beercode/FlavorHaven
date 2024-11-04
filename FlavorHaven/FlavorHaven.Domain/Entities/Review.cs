using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Review : BaseEntity
{
    public string Note { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
}