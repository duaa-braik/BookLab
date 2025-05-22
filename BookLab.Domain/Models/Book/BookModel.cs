using BookLab.Domain.Entities;

namespace BookLab.Domain.Models.Book;

public class BookModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ISBN { get; set; }

    public int? DiscountId { get; set; }

    public decimal Price { get; set; }

    public DateTime PublicationDate { get; set; }

    public int PublisherId { get; set; }

    public int NumberOfPages { get; set; }

    public string Language { get; set; }

    public string? CoverPage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}