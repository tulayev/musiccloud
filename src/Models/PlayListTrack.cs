namespace Models
{
    public class PlayListTrack
    {
        public int Id { get; set; }
        public Guid PlayListId { get; set; }
        public PlayList PlayList { get; set; }
        public Guid TrackId { get; set; }
        public Track Track { get; set; } 
    }
}
