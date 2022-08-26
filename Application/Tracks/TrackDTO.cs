using Application.Profiles;

namespace Application.Tracks
{
    public class TrackDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public Profile Uploader { get; set; }
    }
}