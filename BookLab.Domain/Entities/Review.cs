namespace BookLab.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public Guid ReviewerId { get; set; }

        public User Reviewer { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public string? ReviewText { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
