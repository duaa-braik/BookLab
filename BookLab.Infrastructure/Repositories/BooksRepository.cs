using BookLab.Domain.Entities;
using BookLab.Domain.Repositories;

namespace BookLab.Infrastructure.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly BookLabDbContext _context;

    public BooksRepository(BookLabDbContext context)
    {
        _context = context;
    }

    public void CreateBook(Book book)
    {
        _context.Book.Add(book);
    }
}
