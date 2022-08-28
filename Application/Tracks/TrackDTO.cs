using Application.DTOs;

namespace Application.Tracks
{
    public class TrackDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public AccountDTO Uploader { get; set; }

        public string Poster { get; set; }

        public string Audio { get; set; }
    }
}