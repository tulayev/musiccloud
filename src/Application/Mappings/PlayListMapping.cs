using Application.DTOs.PlayLists;
using AutoMapper;
using Models;

namespace Application.Mappings
{
    public class PlayListMapping : Profile
    {
        public PlayListMapping()
        {
            CreateMap<AddTrackToPlayListDto, PlayListTrack>();

            CreateMap<PlayList, PlayListDto>()
                .ForMember(d => d.Owner, o => o.MapFrom(s => s.User));
        }
    }
}
