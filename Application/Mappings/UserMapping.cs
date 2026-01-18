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

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
