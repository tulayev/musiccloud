using AutoMapper;
using Models;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Track, Track>();
        }
    }
}