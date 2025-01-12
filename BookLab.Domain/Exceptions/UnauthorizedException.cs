using BookLab.Domain.Models;

namespace BookLab.Domain.Exceptions
{
    public class UnauthorizedException : Exception, IGeneralException
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public UnauthorizedException(ErrorModel error) : base(error.Message)
        {
            ErrorCode = error.ErrorCode;
            ErrorMessage = error.Message;
        }
    }
}
