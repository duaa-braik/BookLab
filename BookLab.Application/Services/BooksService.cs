using BookLab.Application.Dtos.Book;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models.Book;
using BookLab.Domain.Repositories;
using MapsterMapper;

namespace BookLab.Application.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BooksService(IMapper mapper, IBooksRepository booksRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _booksRepository = booksRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookModel> CreateBookAsync(CreateBookDto bookDto)
    {
        var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var book = _mapper.Map<Book>(bookDto);

            _booksRepository.CreateBook(book);

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();

            return _mapper.Map<BookModel>(book);
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}
