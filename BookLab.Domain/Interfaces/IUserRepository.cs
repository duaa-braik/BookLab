using BookLab.Domain.Entities;

namespace BookLab.Domain.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
    }
}
