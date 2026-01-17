using Application.DTOs.Tracks;
using AutoMapper;
using Models;

namespace Application.Mappings
{
    public class TrackMapping : Profile
    {
        public TrackMapping()
        {
            CreateMap<Track, TrackDto>()
                .ForMember(d => d.Uploader, o => o.MapFrom(s => s.User));

            CreateMap<CreateTrackDto, Track>();

            CreateMap<UpdateTrackDto, Track>();
        }
    }
}
