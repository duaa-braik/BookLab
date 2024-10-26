using BookLab.Domain.Entities;

namespace BookLab.Domain.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser<TUser>(TUser user) where TUser : class;

        Task<string?> GetUserEmailAsync(string email);
    }
}
