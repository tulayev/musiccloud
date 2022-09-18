namespace Application.DTOs.PlayLists
{
    public class AddTrackToPlayListDTO
    {
        public Guid PlayListId { get; set; }

        public Guid TrackId { get; set; }
    }
}
