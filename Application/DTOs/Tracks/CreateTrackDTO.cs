namespace Application.DTOs.Tracks
{
    public class CreateTrackDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string UserId { get; set; }

        public int? PosterId { get; set; }
        
        public int? AudioId { get; set; }
    }
}