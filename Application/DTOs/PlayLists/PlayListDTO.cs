namespace Application.DTOs.PlayLists
{
    public class PlayListDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public AccountDTO Owner { get; set; }
    }
}