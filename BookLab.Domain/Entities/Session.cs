namespace BookLab.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
