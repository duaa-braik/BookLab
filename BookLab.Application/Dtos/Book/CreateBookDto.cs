namespace BookLab.Application.Dtos.Book;

public class CreateBookDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string ISBN { get; set; }

    public decimal Price { get; set; }

    public int NumberOfPages { get; set; }

    public int PublisherId { get; set; }

    public string Language { get; set; }

    public string CoverPage { get; set; }

    public DateTime PublicationDate { get; set; }

    public int? DiscountId { get; set; }
}