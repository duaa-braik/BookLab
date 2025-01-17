﻿namespace BookLab.Domain.Models;

public class CreateUserResponseModel
{
    public string UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime CreatedAt { get; set; }
}