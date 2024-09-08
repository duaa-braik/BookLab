namespace BookLab.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Email { get; set; }

        public string? Bio { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Nationality { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public User UserCreated { get; set; }

        public Guid? UpdatedBy { get; set; }

        public User? UserUpdated { get; set; }

        public ICollection<AuthorBook> Books { get; set; } = [];
    }
}
