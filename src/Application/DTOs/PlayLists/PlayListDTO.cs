using Application.DTOs.Users;

namespace Application.DTOs.PlayLists
{
    public class PlayListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountDto Owner { get; set; }
    }
}
