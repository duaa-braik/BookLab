using BookLab.Domain.Models;

namespace BookLab.Application.Interfaces
{
    public interface ISessionService
    {
        Task<(string, string)> CreateSessionAsync(CreateUserModel user);
    }
}