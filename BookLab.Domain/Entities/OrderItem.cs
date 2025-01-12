namespace BookLab.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public Guid BookId { get; set; }

        public long OrderId { get; set; }

        public Order Order { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
