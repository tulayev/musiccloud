namespace Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public Guid TrackId { get; set; }
        public Track Track { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
