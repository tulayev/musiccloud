using Application.DTOs.PlayLists;
using Models;

namespace Application.Mappings
{
    public class PlayListMapping : MappingProfiles
    {
        public PlayListMapping()
        {
            CreateMap<AddTrackToPlayListDto, PlayListTrack>();

            CreateMap<PlayList, PlayListDto>()
                .ForMember(d => d.Owner, o => o.MapFrom(s => s.User));
        }
    }
}
