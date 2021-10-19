using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class NewsFileMapper : Profile
    {
        public NewsFileMapper()
        {
            CreateMap<NewsFile, NewsFileAddDto>().ReverseMap();
            CreateMap<NewsFile, NewsFileUpdateDto>().ReverseMap();

            CreateMap<NewsFile, NewsFileDto>()
                .ForMember(f => f.NewsTitle, g => g.MapFrom(t => t.News == null ? "" : t.News.Title))
                .ForMember(f => f.NewsFileTypeEntityName, g => g.MapFrom(t => t.NewsFileTypeEntity == null ? "" : Translator.GetByKey(t.NewsFileTypeEntity.EntityName)))
                .ForMember(f => f.FileName, g => g.MapFrom(t => t.File == null ? "" : t.File.FileName.GetFullFilePath()))
                .ForMember(f => f.CoverFileName, g => g.MapFrom(t => t.VideoCoverFile == null ? "" : t.VideoCoverFile.FileName.GetFullFilePath()))
                .ReverseMap();

            CreateMap<NewsFile, MainPageNewsFileDto>()
                .ForMember(f => f.FileName, g => g.MapFrom(t => t.File == null ? "" : t.File.FileName.GetFullFilePath()))
                .ForMember(f => f.CoverFileName, g => g.MapFrom(t => t.VideoCoverFile == null ? "" : t.VideoCoverFile.FileName.GetFullFilePath()))
                .ReverseMap();

        }
    }
}
