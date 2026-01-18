using Application.DTOs.Comments;
using AutoMapper;
using Models;

namespace Application.Mappings
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Image.Url));
        }
    }
}
