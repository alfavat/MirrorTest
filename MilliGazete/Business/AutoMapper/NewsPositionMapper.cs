using AutoMapper;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using System.Linq;

namespace Business.AutoMapper
{
    public class NewsPositionMapper : Profile
    {
        public NewsPositionMapper()
        {
            CreateMap<NewsPosition, NewsPositionAddDto>().ReverseMap();
            CreateMap<NewsPosition, NewsPositionUpdateDto>().ReverseMap();
            CreateMap<NewsPosition, NewsPositionDto>().ForMember(f => f.News, g => g.MapFrom(u => u.News));

            CreateMap<News, NewsPositionNewsDto>()
              .ForMember(f => f.ShortDescription, g => g.MapFrom(u => u.ShortDescription.GetFirstWords(5)))
              .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFiles != null && g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
              g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));
        }
    }
}
