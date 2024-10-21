namespace BookLab.Domain.Entities
{
    public class Admin : User
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
