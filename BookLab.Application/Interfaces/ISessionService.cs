using BookLab.Domain.Entities;

namespace BookLab.Application.Interfaces
{
    public interface ISessionService
    {
        Task CreateSessionAsync(User user, string refreshToken);
    }
}