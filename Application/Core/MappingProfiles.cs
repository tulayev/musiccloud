using Application.DTOs.Tracks;
using Application.DTOs;
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

            CreateMap<CreateTrackDTO, Track>();
            
            CreateMap<EditTrackDTO, Track>();

            CreateMap<PlayList, PlayListDTO>()
                .ForMember(d => d.Owner, o => o.MapFrom(s => s.User));
            
            CreateMap<User, AccountDTO>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Image.Url));

            CreateMap<Comment, CommentDTO>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Image));
        }
    }
}