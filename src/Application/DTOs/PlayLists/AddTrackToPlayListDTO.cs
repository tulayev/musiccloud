namespace Application.DTOs.PlayLists
{
    public class AddTrackToPlayListDto
    {
        public Guid PlayListId { get; set; }
        public Guid TrackId { get; set; }
    }
}
