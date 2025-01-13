using BookLab.Domain.Models.Error;

namespace BookLab.Domain.Exceptions;

public class NotFoundException : Exception, IGeneralException
{
    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    public NotFoundException(ErrorModel error) : base(error.Message)
    {
        ErrorCode = error.ErrorCode;
        ErrorMessage = error.Message;
    }
}