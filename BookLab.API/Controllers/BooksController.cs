using BookLab.Application.Dtos.Book;
using BookLab.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BookLab.API.Controllers;

[Route("api")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpPost("books")]
    public async Task<ActionResult<CreateBookResponseDto>> CreateBook(CreateBookDto book)
    {
        string adminToken = Request.Headers.Authorization.ToString().Split(" ").Last();

        var createdBook = await _booksService.CreateBookAsync(book, adminToken);

        var createdBookDto = createdBook.Adapt<CreateBookResponseDto>();

        return Ok(createdBookDto);
    }
}
