using BookLab.Domain.Entities;

namespace BookLab.Domain.Repositories
{
    public interface IBooksRepository
    {
        void CreateBook(Book book);
    }
}