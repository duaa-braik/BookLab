using BookLab.Domain.Models;

namespace BookLab.Domain.Exceptions;

public class ConflictException : Exception, IGeneralException
{
    public string ErrorMessage { get; set; }

    public string ErrorCode { get; set; }

    public ConflictException(ErrorModel error) : base(error.Message)
    {
        ErrorMessage = error.Message;
        ErrorCode = error.ErrorCode;
    }
}
