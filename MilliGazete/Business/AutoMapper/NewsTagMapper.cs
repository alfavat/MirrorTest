using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class NewsTagMapper : Profile
    {
        public NewsTagMapper()
        {
            CreateMap<NewsTag, NewsTagAddDto>().ReverseMap();
            CreateMap<NewsTag, NewsTagUpdateDto>().ReverseMap();
            CreateMap<NewsTag, NewsTagDto>()
                .ForMember(f => f.TagName, g => g.MapFrom(t => t.Tag == null ? "" : t.Tag.TagName))
                .ForMember(f => f.Url, g => g.MapFrom(t => t.Tag == null ? "" : t.Tag.Url))
                .ReverseMap();
        }
    }
}
