namespace Application.DTOs.Tracks
{
    public class UpdateTrackDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
