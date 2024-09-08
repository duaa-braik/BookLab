namespace BookLab.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public decimal Total { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
