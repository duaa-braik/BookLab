using BookLab.Domain.Models;

namespace BookLab.Application.Interfaces
{
    public interface ISessionService
    {
        Task<(string, string)> CreateSessionAsync(UserModel user);
        Task<SessionModel> RefreshSessionAsync(string token);
    }
}