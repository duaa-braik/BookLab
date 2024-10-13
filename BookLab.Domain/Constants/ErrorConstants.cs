﻿namespace BookLab.Domain.Constants;

public class ErrorConstants
{
    public enum ErrorType
    {
        ROLE_NOT_FOUND,
        ACCOUNT_EXISTS
    }

    public static readonly Dictionary<ErrorType, string> Errors = new() 
    {
        { ErrorType.ROLE_NOT_FOUND, "Role does not exist" },
        { ErrorType.ACCOUNT_EXISTS, "The email you provided is already associated with an account" }
    };
}