﻿using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; }
    public DateTime CreatedTime { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public string Address { get; set; }
    public bool IsPayed { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid StatusId { get; set; }
    public OrderStatus Status { get; set; }
    
    public ICollection<OrderItems> OrderItems { get; set; }
}