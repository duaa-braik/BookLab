namespace BookLab.Domain.Entities
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string Location { get; set; }

        public DateOnly FoundedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public Guid CreatedBy { get; set; }

        public Admin AdminCreated { get; set; }
 
        public Guid? UpdatedBy { get; set; }

        public Admin? AdminUpdated { get; set; }

        public ICollection<Book> Books { get; set; } = [];
    }
}
