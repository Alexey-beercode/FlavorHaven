﻿namespace FlavorHaven.Application.Models.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
}