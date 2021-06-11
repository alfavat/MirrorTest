using AutoMapper;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using System.Collections.Generic;

namespace Business.AutoMapper
{
    public class AgencyNewsMapper : Profile
    {
        public AgencyNewsMapper()
        {
            CreateMap<NewsAgencyAddDto, AgencyNews>().ForMember(f => f.AgencyNewsFiles, g => g.Ignore()).ForMember(f => f.NewsAgencyEntityId, g => g.MapFrom(u => (int)u.NewsAgencyEntityId));
            CreateMap<AgencyNews, AgencyNewsViewDto>().ForMember(f => f.AgencyNewsFileList, g => g.MapFrom(u => u.AgencyNewsFiles));

            CreateMap<AgencyNews, NewsAddDto>()
                .BeforeMap((copyDto, addDto) =>
                {
                    addDto.Active = true;
                    addDto.IsDraft = true;

                    addDto.NewsPropertyList = new List<NewsPropertyAddDto>();
                    addDto.NewsRelatedNewsList = new List<NewsRelatedNewsAddDto>();
                    addDto.NewsTagList = new List<NewsTagAddDto>();
                    addDto.NewsTypeEntityId = (int)NewsTypeEntities.News;
                    addDto.NewsId = 0;
                })
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Title.ToUrlFormat().ReplaceSpecialCharacters()))
                .ForMember(f => f.SocialTitle, g => g.MapFrom(u => u.Title))
                .ForMember(f => f.SeoTitle, g => g.MapFrom(u => u.Title))
                .ForMember(f => f.Title, g => g.MapFrom(u => u.Title))
                .ForMember(f => f.SocialDescription, g => g.MapFrom(u => u.Description.ParseHtml()))
                .ForMember(f => f.ShortDescription, g => g.MapFrom(u => u.Description.ParseHtml()))
                .ForMember(f => f.HtmlContent, g => g.MapFrom(u => u.Description))
                .ForMember(f => f.SeoDescription, g => g.MapFrom(u => u.Description.ParseHtml()))
                .ForMember(f => f.PublishDate, g => g.MapFrom(u => u.PublishDate.HasValue ? u.PublishDate.ToString() : null))
                .ForMember(f => f.PublishTime, g => g.MapFrom(u => u.PublishDate.HasValue ? u.PublishDate.Value.TimeOfDay.ToString() : null));
        }
    }
}
