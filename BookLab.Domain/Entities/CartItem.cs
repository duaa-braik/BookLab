namespace BookLab.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public Guid CartId { get; set; }

        public Cart Cart { get; set; }

        public int LineItemId { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
