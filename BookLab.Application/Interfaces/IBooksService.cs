using BookLab.Application.Dtos.Book;
using BookLab.Domain.Models.Book;

namespace BookLab.Application.Interfaces;

public interface IBooksService
{
    Task<BookModel> CreateBookAsync(CreateBookDto bookDto);
}
