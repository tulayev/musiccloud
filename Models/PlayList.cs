namespace Models
{
    public class PlayList
    {
        public Guid Id { get; set; }

        public string Name { get; set; }   

        public User User { get; set; }

        public List<PlayListTrack> Tracks { get; set; }
    }
}