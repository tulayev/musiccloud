namespace Application.DTOs.Tracks
{
    public class EditTrackDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }
    }
}