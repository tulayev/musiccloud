namespace Models
{
    public class Comment
    {
        public int Id { get; set; }        

        public string Body { get; set; }

        public User Author { get; set; }

        public Track Track { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}