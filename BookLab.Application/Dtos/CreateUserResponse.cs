﻿namespace BookLab.Application.Dtos
{
    public class CreateUserResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
