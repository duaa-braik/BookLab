namespace BookLab.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<User> Users { get; set; } = [];
        
        public Guid CreatedBy { get; set; }

        public User UserCreated { get; set; }

        public Guid? UpdatedBy { get; set; }
        
        public User? UserUpdated { get; set; }
    }
}
