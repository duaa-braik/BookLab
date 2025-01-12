namespace BookLab.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int CartStatusID { get; set; }

        public CartStatus CartStatus { get; set; } 

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}
