using Models;

namespace Application.DTOs.Tracks
{
    public class TrackDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int? PosterId { get; set; }
        public int? AudioId { get; set; }
        public AccountDTO Uploader { get; set; }
        public AppFile Poster { get; set; }
        public AppFile Audio { get; set; }
    }
}