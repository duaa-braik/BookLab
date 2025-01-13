using BookLab.Domain.Entities;
using BookLab.Domain.Models.Session;
using BookLab.Domain.Models.User;

namespace BookLab.Application.Interfaces
{
    public interface ISessionService
    {
        Task<(string, string)> CreateSessionAsync(UserModel user);
        Task DeleteSessionAsync(Session session);
        Task<Session> GetSessionByUserIdAsync(string userId);
        Task<SessionModel> RefreshSessionAsync(string token);
    }
}