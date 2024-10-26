namespace BookLab.Domain.Entities
{
    public class Customer : User
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public ICollection<Review> Reviews { get; set; } = [];

        public ICollection<Order> Orders { get; set; }
    }
}
