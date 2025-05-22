namespace BookLab.Application.Dtos.Book;

public class CreateBookResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ISBN { get; set; }

    public string? Discount { get; set; }

    public decimal Price { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Publisher { get; set; }

    public int NumberOfPages { get; set; }

    public string Language { get; set; }

    public string? CoverPage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
