using BookLab.Domain.Models;
using static BookLab.Domain.Constants.ErrorConstants;

namespace BookLab.Application.Factories;

public class ErrorFactory : IErrorFactory
{
    public ErrorModel Create(ErrorType errorCode)
    {
        return new ErrorModel { Message = Errors[errorCode], ErrorCode = errorCode.ToString() };
    }
}
