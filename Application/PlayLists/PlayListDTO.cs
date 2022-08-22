using Application.Profiles;

namespace Application.PlayLists
{
    public class PlayListDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Profile Owner { get; set; }
    }
}