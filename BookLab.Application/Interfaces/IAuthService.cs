﻿using BookLab.API.Dtos;

namespace BookLab.Application.Interfaces;

public interface IAuthService
{
    void CreateUserAsync(CreateUserRequest request);
}
