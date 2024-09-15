namespace BookLab.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Review> Reviews { get; set; } = [];

        public ICollection<Order> Orders { get; set; }
    }
}
