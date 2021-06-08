using AutoMapper;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using System.Linq;

namespace Business.AutoMapper
{
    public class NewsBookmarkMapper : Profile
    {
        public NewsBookmarkMapper()
        {
            CreateMap<NewsBookmark, NewsBookmarkAddDto>().ReverseMap();
            CreateMap<NewsBookmark, NewsBookmarkUpdateDto>().ReverseMap();
            CreateMap<NewsBookmark, NewsBookmarkDto>()
                .ForMember(f => f.ShortDescription, g => g.MapFrom(t => t.News == null ? "" : t.News.ShortDescription))
                .ForMember(f => f.Title, g => g.MapFrom(t => t.News == null ? "" : t.News.Title))
                .ForMember(f => f.FullName, g => g.MapFrom(t => t.User == null ? "" : t.User.FirstName + " " + t.User.LastName))
                .ForMember(f => f.Url, g => g.MapFrom(t => t.News == null ? "" : t.News.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.News == null ? "" :
                                g.News.NewsFile != null &&
                                g.News.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.News.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));
        }
    }
}
