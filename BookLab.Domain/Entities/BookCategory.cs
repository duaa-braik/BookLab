namespace BookLab.Domain.Entities
{
    public class BookCategory
    {
        public int Id { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
