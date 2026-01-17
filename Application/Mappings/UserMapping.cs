using Application.DTOs.Users;
using AutoMapper;
using Models;

namespace Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, AccountDto>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Image.Url));
        }
    }
}
