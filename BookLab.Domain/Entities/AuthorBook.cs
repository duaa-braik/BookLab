namespace BookLab.Domain.Entities
{
    public class AuthorBook
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
