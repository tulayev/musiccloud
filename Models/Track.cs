namespace Models
{
    public class Track
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public User User { get; set; }

        // public AppFile Poster { get; set; }

        // public AppFile Audio { get; set; }

        public ICollection<PlayListTrack> PlayLists { get; set; } = new List<PlayListTrack>();
    }
}