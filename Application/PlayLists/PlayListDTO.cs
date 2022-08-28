using Application.DTOs;

namespace Application.PlayLists
{
    public class PlayListDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public AccountDTO Owner { get; set; }
    }
}