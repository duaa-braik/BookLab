namespace BookLab.Domain.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public Guid CreatedBy { get; set; }

        public User UserCreated { get; set; }

        public Guid? UpdatedBy { get; set; }

        public User? UserUpdated { get; set; }
    }
}
