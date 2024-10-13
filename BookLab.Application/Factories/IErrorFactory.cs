using BookLab.Domain.Models;
using static BookLab.Domain.Constants.ErrorConstants;

namespace BookLab.Application.Factories;

public interface IErrorFactory
{
    ErrorModel Create(ErrorType errorCode);
}
