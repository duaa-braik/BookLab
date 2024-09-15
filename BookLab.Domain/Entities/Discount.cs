namespace BookLab.Domain.Entities
{
    public class Discount
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double DiscountPercentage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public Admin AdminCreated { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Admin? AdminUpdated { get; set; }
    }
}
