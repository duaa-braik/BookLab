namespace BookLab.Domain.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
