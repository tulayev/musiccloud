using Application.PlayLists;
using Application.Tracks;
using AutoMapper;
using Models;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Track, TrackDTO>()
                .ForMember(d => d.Uploader, o => o.MapFrom(s => s.User));
            CreateMap<PlayList, PlayListDTO>()
                .ForMember(d => d.Owner, o => o.MapFrom(s => s.User));
            CreateMap<User, Profiles.Profile>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.Bio));
        }
    }
}