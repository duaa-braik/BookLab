﻿namespace BookLab.Domain.Models;

public class CreateUserResponseModel
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public DateTime CreatedAt { get; set; }
}