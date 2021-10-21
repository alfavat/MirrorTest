using AutoMapper;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using System.Linq;

namespace Business.AutoMapper
{
    public class NewsRelatedNewsMapper : Profile
    {
        public NewsRelatedNewsMapper()
        {
            CreateMap<NewsRelatedNews, NewsRelatedNewsAddDto>().ReverseMap();
            CreateMap<NewsRelatedNews, NewsRelatedNewsUpdateDto>().ReverseMap();
            CreateMap<News, RelatedNewsViewDto>().ForMember(f => f.AddUser, u => u.MapFrom(g => g.AddUser == null ? "" : g.AddUser.UserName))
              .ForMember(f => f.UpdateUser, u => u.MapFrom(g => g.UpdateUser == null ? "" : g.UpdateUser.UserName))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              // .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFile != null && g.NewsFile.Any(t => t.File.FileType.ToLower().Contains("image")) ? g.NewsFile.FirstOrDefault(t => t.File.FileType.ToLower().Contains("image")).File.GetFullFilePath() : null));
              .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFiles != null && g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
              g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));


            CreateMap<NewsRelatedNews, NewsRelatedNewsViewDto>().ForMember(f => f.RelatedNews, g => g.MapFrom(t => t.RelatedNews));

            CreateMap<NewsRelatedNews, MainPageRelatedNewsDto>()
                .ForMember(f => f.RelatedNews, g => g.MapFrom(t => t.RelatedNews));

            CreateMap<News, MainPageRelatedNewsDetailsDto>()
                .ForMember(f => f.Url, g => g.MapFrom(t => t.Url.GetUrl(t.HistoryNo, t.NewsTypeEntityId, t.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFiles != null && g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
              g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));
        }
    }
}
