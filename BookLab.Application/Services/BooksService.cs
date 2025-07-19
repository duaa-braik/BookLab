using BookLab.Application.Dtos.Book;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models.Book;
using BookLab.Domain.Repositories;
using MapsterMapper;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public BooksService(IMapper mapper, IBooksRepository booksRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _mapper = mapper;
        _booksRepository = booksRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<BookModel> CreateBookAsync(CreateBookDto bookDto, string adminToken)
    {
        var transaction = _unitOfWork.BeginTransaction();

        var adminId = _tokenService.GetClaimFromJwtToken(adminToken, ClaimType.UserId);

        try
        {
            var book = _mapper.Map<Book>(bookDto);

            book.CreatedBy = Guid.Parse(adminId);
            book.CreatedAt = DateTime.UtcNow;

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
