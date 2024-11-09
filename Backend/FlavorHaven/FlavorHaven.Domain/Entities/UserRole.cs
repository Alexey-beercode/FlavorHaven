﻿using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}