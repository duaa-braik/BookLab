namespace BookLab.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

        public int? DiscountId { get; set; }

        public Discount? Discount { get; set; }

        public decimal Price { get; set; }

        public DateTime PublicationDate { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public string Language { get; set; }

        public string? CoverPage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public User UserCreated { get; set; }

        public Guid? UpdatedBy { get; set; }

        public User? UserUpdated { get; set; }

        public ICollection<AuthorBook> Authors { get; set; } = [];

        public ICollection<BookCategory> Categories { get; set; } = [];
    }
}
