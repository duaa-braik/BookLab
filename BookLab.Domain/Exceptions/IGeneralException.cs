namespace BookLab.Domain.Exceptions;

public interface IGeneralException
{
    string ErrorMessage { get; set; }

    string ErrorCode { get; set; }
}